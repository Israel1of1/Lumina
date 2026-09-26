/* ============================================================================
   LUMINA — FASE PROTOTIPO · DDL + SEED
   Motor : MySQL 8.0+ (InnoDB / utf8mb4). Requiere 8.0.16+ para que los CHECK
           se hagan cumplir; en versiones anteriores se ignoran.
   Base  : LUMINA (se crea si no existe)
   Tablas: 27 (Seguridad 4 · Catálogos y entidades 4 · Perfil del estudiante 1 ·
            Base de Conocimientos 1 · Administración 3 · Planificación 7 ·
            Monitoreo 7)
   Idempotente: si se reejecuta, recrea la base de datos y las 27 tablas.
   Referencias: fases/FASE_PROTOTIPO.md, 04_PROPUESTA_DEFINITIVA.md,
                05_PLAN_FASES.md (paso 1: DDL)
   Nota: los nombres de tabla/columna usan el mismo PascalCase que la versión
   SQL Server para mantener los scripts alineados (en MySQL las mayúsculas en
   nombres no distinguen; se usan comillas graves por si el servidor se
   configura con lower_case_table_names sensible).
   La cuota RN-01 (máx. 10 estudiantes por grupo) se valida por la aplicación
   (bloque de trigger opcional al final).
   ============================================================================ */

CREATE DATABASE IF NOT EXISTS LUMINA
    DEFAULT CHARACTER SET utf8mb4
    DEFAULT COLLATE utf8mb4_unicode_ci;
USE LUMINA;

/* ----------------------------------------------------------------------------
   0. LIMPIEZA — orden inverso al de creación (recomendado: no ejecutar contra
      una base con datos reales)
   ---------------------------------------------------------------------------- */
DROP TABLE IF EXISTS `RoutineLog`;
DROP TABLE IF EXISTS `RoutineDetail`;
DROP TABLE IF EXISTS `HabitCompliance`;
DROP TABLE IF EXISTS `Routine`;
DROP TABLE IF EXISTS `StudentHabit`;
DROP TABLE IF EXISTS `StudyHistory`;
DROP TABLE IF EXISTS `StudentProgress`;
DROP TABLE IF EXISTS `PecsCard`;
DROP TABLE IF EXISTS `PecsBoard`;
DROP TABLE IF EXISTS `ContentKeyword`;
DROP TABLE IF EXISTS `LearningContent`;
DROP TABLE IF EXISTS `LessonStep`;
DROP TABLE IF EXISTS `Lesson`;
DROP TABLE IF EXISTS `Module`;
DROP TABLE IF EXISTS `StudentInterest`;
DROP TABLE IF EXISTS `EntityStudentRelation`;
DROP TABLE IF EXISTS `GroupSubject`;
DROP TABLE IF EXISTS `Student`;
DROP TABLE IF EXISTS `Guardian`;
DROP TABLE IF EXISTS `Teacher`;
DROP TABLE IF EXISTS `LinkCode`;
DROP TABLE IF EXISTS `ClassGroup`;
DROP TABLE IF EXISTS `Subject`;
DROP TABLE IF EXISTS `Keyword`;
DROP TABLE IF EXISTS `UserRole`;
DROP TABLE IF EXISTS `Role`;
DROP TABLE IF EXISTS `User`;

/* ============================================================================
   1. SEGURIDAD (4)
   ============================================================================ */

/* 1.1 User — sin User.role (decisión D3: roles en UserRole) */
CREATE TABLE `User` (
    `id`           INT          NOT NULL AUTO_INCREMENT,
    `email`        VARCHAR(120) NOT NULL,
    `nationalId`   VARCHAR(16)  NULL,
    `passwordHash` VARCHAR(255) NOT NULL,
    `isActive`     TINYINT(1)   NOT NULL DEFAULT 1,
    `lastLoginAt`  DATETIME     NULL,
    `createdAt`    DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt`    DATETIME     NULL,
    PRIMARY KEY (`id`),
    UNIQUE KEY `UQ_User_email` (`email`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 1.2 Role — seed: INSTITUTION, TEACHER, GUARDIAN */
CREATE TABLE `Role` (
    `id`          INT          NOT NULL AUTO_INCREMENT,
    `name`        VARCHAR(50)  NOT NULL,
    `description` VARCHAR(150) NULL,
    `isActive`    TINYINT(1)   NOT NULL DEFAULT 1,
    `createdAt`   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt`   DATETIME     NULL,
    PRIMARY KEY (`id`),
    UNIQUE KEY `UQ_Role_name` (`name`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 1.3 UserRole — vínculo N:M usuario ↔ rol */
CREATE TABLE `UserRole` (
    `id`         INT      NOT NULL AUTO_INCREMENT,
    `userId`     INT      NOT NULL,
    `roleId`     INT      NOT NULL,
    `assignedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    UNIQUE KEY `UQ_UserRole_user_role` (`userId`, `roleId`),
    CONSTRAINT `FK_UserRole_user` FOREIGN KEY (`userId`) REFERENCES `User` (`id`),
    CONSTRAINT `FK_UserRole_role` FOREIGN KEY (`roleId`) REFERENCES `Role` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 1.4 LinkCode — código de activación emitido por la institución (D10) */
CREATE TABLE `LinkCode` (
    `id`         INT          NOT NULL AUTO_INCREMENT,
    `code`       VARCHAR(50)  NOT NULL,
    `purpose`    VARCHAR(20)  NOT NULL,
    `status`     VARCHAR(20)  NOT NULL DEFAULT 'PENDING',
    `issuedById` INT          NULL,
    `expiresAt`  DATETIME     NULL,
    `usedById`   INT          NULL,
    `usedAt`     DATETIME     NULL,
    `createdAt`  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt`  DATETIME     NULL,
    PRIMARY KEY (`id`),
    UNIQUE KEY `UQ_LinkCode_code` (`code`),
    CONSTRAINT `CK_LinkCode_purpose` CHECK (`purpose` IN ('ENROLLMENT', 'TEACHER_CONTRACT')),
    CONSTRAINT `CK_LinkCode_status`  CHECK (`status` IN ('PENDING', 'USED', 'EXPIRED', 'REVOKED')),
    CONSTRAINT `FK_LinkCode_issuedBy` FOREIGN KEY (`issuedById`) REFERENCES `User` (`id`),
    CONSTRAINT `FK_LinkCode_usedBy`   FOREIGN KEY (`usedById`)   REFERENCES `User` (`id`) ON DELETE SET NULL
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* ============================================================================
   2. CATÁLOGOS Y ENTIDADES (4) + PERFIL DEL ESTUDIANTE (1)
   ============================================================================ */

/* 2.1 Teacher — userId desacoplado y opcional (D1) */
CREATE TABLE `Teacher` (
    `id`              INT           NOT NULL AUTO_INCREMENT,
    `userId`          INT           NULL,
    `firstName`       VARCHAR(100)  NOT NULL,
    `lastName`        VARCHAR(100)  NOT NULL,
    `nationalId`      VARCHAR(16)   NULL,
    `personalEmail`   VARCHAR(120)  NULL,
    `phone`           VARCHAR(20)   NULL,
    `address`         VARCHAR(200)  NULL,
    `city`            VARCHAR(100)  NULL,
    `photo`           VARCHAR(255)  NULL,
    `specialty`       VARCHAR(100)  NULL,
    `degree`          VARCHAR(100)  NULL,
    `entityStatus`    VARCHAR(20)   NOT NULL DEFAULT 'ACTIVE',
    `dismissalDate`   DATE          NULL,
    `dismissalReason` TEXT          NULL,
    `createdAt`       DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt`       DATETIME      NULL,
    PRIMARY KEY (`id`),
    UNIQUE KEY `UQ_Teacher_nationalId` (`nationalId`),
    CONSTRAINT `CK_Teacher_entityStatus` CHECK (`entityStatus` IN ('ACTIVE', 'ON_LEAVE', 'INACTIVE')),
    CONSTRAINT `FK_Teacher_user` FOREIGN KEY (`userId`) REFERENCES `User` (`id`) ON DELETE SET NULL
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 2.2 Guardian — userId desacoplado y opcional (D1) */
CREATE TABLE `Guardian` (
    `id`              INT           NOT NULL AUTO_INCREMENT,
    `userId`          INT           NULL,
    `firstName`       VARCHAR(100)  NOT NULL,
    `lastName`        VARCHAR(100)  NULL,
    `nationalId`      VARCHAR(16)   NULL,
    `personalEmail`   VARCHAR(120)  NULL,
    `phone`           VARCHAR(20)   NULL,
    `address`         VARCHAR(200)  NULL,
    `city`            VARCHAR(100)  NULL,
    `photo`           VARCHAR(255)  NULL,
    `relationship`    VARCHAR(50)   NULL,
    `entityStatus`    VARCHAR(20)   NOT NULL DEFAULT 'ACTIVE',
    `dismissalDate`   DATE          NULL,
    `dismissalReason` TEXT          NULL,
    `createdAt`       DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt`       DATETIME      NULL,
    PRIMARY KEY (`id`),
    UNIQUE KEY `UQ_Guardian_nationalId` (`nationalId`),
    CONSTRAINT `FK_Guardian_user` FOREIGN KEY (`userId`) REFERENCES `User` (`id`) ON DELETE SET NULL
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 2.3 Student — pertenece a un ClassGroup; cuenta opcional */
CREATE TABLE `Student` (
    `id`            INT           NOT NULL AUTO_INCREMENT,
    `groupId`       INT           NOT NULL,
    `userId`        INT           NULL,
    `firstName`     VARCHAR(100)  NOT NULL,
    `lastName`      VARCHAR(100)  NULL,
    `uniqueNumber`  VARCHAR(30)   NULL,
    `birthDate`     DATE          NULL,
    `gender`        VARCHAR(10)   NULL,
    `languageLevel` VARCHAR(100)  NULL,
    `clinicalInfo`  TEXT          NULL,
    `observations`  TEXT          NULL,
    `isActive`      TINYINT(1)    NOT NULL DEFAULT 1,
    `createdAt`     DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt`     DATETIME      NULL,
    PRIMARY KEY (`id`),
    UNIQUE KEY `UQ_Student_uniqueNumber` (`uniqueNumber`),
    KEY `IX_Student_groupId` (`groupId`),
    CONSTRAINT `FK_Student_group` FOREIGN KEY (`groupId`) REFERENCES `ClassGroup` (`id`),
    CONSTRAINT `FK_Student_user`  FOREIGN KEY (`userId`)  REFERENCES `User` (`id`) ON DELETE SET NULL
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 2.4 EntityStudentRelation — vínculo polimórfico docente/tutor ↔ estudiante */
CREATE TABLE `EntityStudentRelation` (
    `id`           INT          NOT NULL AUTO_INCREMENT,
    `entityId`     INT          NOT NULL,
    `entityType`   VARCHAR(20)  NOT NULL,
    `studentId`    INT          NOT NULL,
    `relationType` VARCHAR(20)  NOT NULL,
    `isActive`     TINYINT(1)   NOT NULL DEFAULT 1,
    `assignedAt`   DATE         NULL,
    `endDate`      DATE         NULL,
    `createdAt`    DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    KEY `IX_EntityStudentRelation_studentId` (`studentId`),
    KEY `IX_EntityStudentRelation_entity` (`entityType`, `entityId`),
    CONSTRAINT `CK_EntityStudentRelation_entityType`   CHECK (`entityType` IN ('TEACHER', 'GUARDIAN')),
    CONSTRAINT `CK_EntityStudentRelation_relationType` CHECK (`relationType` IN ('PRIMARY_GUARDIAN', 'GUARDIAN', 'REFERRING_TEACHER')),
    CONSTRAINT `FK_EntityStudentRelation_student` FOREIGN KEY (`studentId`) REFERENCES `Student` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 2.5 StudentInterest — gustos/reforzadores: lista N registros por estudiante */
CREATE TABLE `StudentInterest` (
    `id`          INT           NOT NULL AUTO_INCREMENT,
    `studentId`   INT           NOT NULL,
    `name`        VARCHAR(100)  NULL,
    `description` TEXT          NULL,
    `createdAt`   DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt`   DATETIME      NULL,
    PRIMARY KEY (`id`),
    KEY `IX_StudentInterest_studentId` (`studentId`),
    CONSTRAINT `FK_StudentInterest_student` FOREIGN KEY (`studentId`) REFERENCES `Student` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* ============================================================================
   3. BASE DE CONOCIMIENTOS (1)
   ============================================================================ */

/* 3.1 Keyword — diccionario; lo consume ContentKeyword (se reutiliza en el
      Producto final para la búsqueda de artículos) */
CREATE TABLE `Keyword` (
    `id`        INT          NOT NULL AUTO_INCREMENT,
    `name`      VARCHAR(100) NOT NULL,
    `createdAt` DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    UNIQUE KEY `UQ_Keyword_name` (`name`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* ============================================================================
   4. ADMINISTRACIÓN (3)
   ============================================================================ */

/* 4.1 Subject — catálogo de materias */
CREATE TABLE `Subject` (
    `id`          INT           NOT NULL AUTO_INCREMENT,
    `name`        VARCHAR(100)  NOT NULL,
    `description` TEXT          NULL,
    `color`       VARCHAR(20)   NULL,
    `icon`        VARCHAR(100)  NULL,
    `createdAt`   DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 4.2 ClassGroup — schoolYearId se difiere al MVP (con la tabla SchoolYear) */
CREATE TABLE `ClassGroup` (
    `id`          INT           NOT NULL AUTO_INCREMENT,
    `name`        VARCHAR(120)  NOT NULL,
    `gradeLevel`  VARCHAR(50)   NULL,
    `description` TEXT          NULL,
    `isActive`    TINYINT(1)    NOT NULL DEFAULT 1,
    `createdAt`   DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 4.3 GroupSubject — asignación docente + materia a un grupo (historial; D4) */
CREATE TABLE `GroupSubject` (
    `id`             INT      NOT NULL AUTO_INCREMENT,
    `groupId`        INT      NOT NULL,
    `subjectId`      INT      NOT NULL,
    `teacherId`      INT      NOT NULL,
    `isActive`       TINYINT(1) NOT NULL DEFAULT 1,
    `assignmentDate` DATE     NULL,
    `endDate`        DATE     NULL,
    `createdAt`      DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    UNIQUE KEY `UQ_GroupSubject` (`groupId`, `subjectId`, `teacherId`, `assignmentDate`),
    KEY `IX_GroupSubject_teacherId` (`teacherId`),
    CONSTRAINT `FK_GroupSubject_group`   FOREIGN KEY (`groupId`)   REFERENCES `ClassGroup` (`id`),
    CONSTRAINT `FK_GroupSubject_subject` FOREIGN KEY (`subjectId`) REFERENCES `Subject` (`id`),
    CONSTRAINT `FK_GroupSubject_teacher` FOREIGN KEY (`teacherId`) REFERENCES `Teacher` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* ============================================================================
   5. PLANIFICACIÓN (7)
   ============================================================================ */

/* 5.1 Module — estructura de la materia */
CREATE TABLE `Module` (
    `id`          INT           NOT NULL AUTO_INCREMENT,
    `subjectId`   INT           NOT NULL,
    `name`        VARCHAR(120)  NOT NULL,
    `description` TEXT          NULL,
    `iconUrl`     VARCHAR(255)  NULL,
    `createdAt`   DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    KEY `IX_Module_subjectId` (`subjectId`),
    CONSTRAINT `FK_Module_subject` FOREIGN KEY (`subjectId`) REFERENCES `Subject` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 5.2 Lesson — lecciones por módulo */
CREATE TABLE `Lesson` (
    `id`              INT           NOT NULL AUTO_INCREMENT,
    `moduleId`        INT           NOT NULL,
    `title`           VARCHAR(150)  NOT NULL,
    `description`     TEXT          NULL,
    `type`            VARCHAR(20)   NULL,
    `durationMinutes` INT           NULL,
    `createdAt`       DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    KEY `IX_Lesson_moduleId` (`moduleId`),
    CONSTRAINT `FK_Lesson_module` FOREIGN KEY (`moduleId`) REFERENCES `Module` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 5.3 LessonStep — pasos de una lección */
CREATE TABLE `LessonStep` (
    `id`          INT           NOT NULL AUTO_INCREMENT,
    `lessonId`    INT           NOT NULL,
    `stepNumber`  INT           NULL,
    `title`       VARCHAR(150)  NULL,
    `description` TEXT          NULL,
    `contentType` VARCHAR(20)   NULL,
    `contentUrl`  VARCHAR(255)  NULL,
    `isActive`    TINYINT(1)    NOT NULL DEFAULT 1,
    `createdAt`   DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    KEY `IX_LessonStep_lessonId` (`lessonId`),
    CONSTRAINT `FK_LessonStep_lesson` FOREIGN KEY (`lessonId`) REFERENCES `Lesson` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 5.4 LearningContent — diccionario / rutina / excepción. Sin cuestionarios
      (D19): no existe tipo 'question' ni AnswerOption */
CREATE TABLE `LearningContent` (
    `id`           INT           NOT NULL AUTO_INCREMENT,
    `lessonId`     INT           NOT NULL,
    `title`        VARCHAR(150)  NULL,
    `description`  TEXT          NULL,
    `type`         VARCHAR(20)   NULL,
    `isDictionary` TINYINT(1)    NOT NULL DEFAULT 0,
    `isRoutine`    TINYINT(1)    NOT NULL DEFAULT 0,
    `isException`  TINYINT(1)    NOT NULL DEFAULT 0,
    `subjectId`    INT           NULL,
    `level`        VARCHAR(20)   NULL,
    `isActive`     TINYINT(1)    NOT NULL DEFAULT 1,
    `createdAt`    DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    KEY `IX_LearningContent_subjectId` (`subjectId`),
    CONSTRAINT `CK_LearningContent_type` CHECK (`type` IN ('DICTIONARY', 'ROUTINE', 'EXCEPTION')),
    CONSTRAINT `FK_LearningContent_lesson`  FOREIGN KEY (`lessonId`)   REFERENCES `Lesson` (`id`),
    CONSTRAINT `FK_LearningContent_subject` FOREIGN KEY (`subjectId`)  REFERENCES `Subject` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 5.5 ContentKeyword — vínculo contenido ↔ palabra clave */
CREATE TABLE `ContentKeyword` (
    `id`        INT NOT NULL AUTO_INCREMENT,
    `contentId` INT NOT NULL,
    `keywordId` INT NOT NULL,
    PRIMARY KEY (`id`),
    UNIQUE KEY `UQ_ContentKeyword` (`contentId`, `keywordId`),
    KEY `IX_ContentKeyword_keywordId` (`keywordId`),
    CONSTRAINT `FK_ContentKeyword_content` FOREIGN KEY (`contentId`) REFERENCES `LearningContent` (`id`),
    CONSTRAINT `FK_ContentKeyword_keyword` FOREIGN KEY (`keywordId`) REFERENCES `Keyword` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 5.6 PecsBoard — tablero PECS por estudiante */
CREATE TABLE `PecsBoard` (
    `id`          INT           NOT NULL AUTO_INCREMENT,
    `studentId`   INT           NOT NULL,
    `name`        VARCHAR(100)  NULL,
    `description` TEXT          NULL,
    `createdAt`   DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    KEY `IX_PecsBoard_studentId` (`studentId`),
    CONSTRAINT `FK_PecsBoard_student` FOREIGN KEY (`studentId`) REFERENCES `Student` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 5.7 PecsCard — tarjeta PECS por tablero */
CREATE TABLE `PecsCard` (
    `id`          INT           NOT NULL AUTO_INCREMENT,
    `boardId`     INT           NOT NULL,
    `title`       VARCHAR(100)  NULL,
    `imageUrl`    VARCHAR(255)  NULL,
    `audioUrl`    VARCHAR(255)  NULL,
    `category`    VARCHAR(100)  NULL,
    `orderNumber` INT           NULL,
    `createdAt`   DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    KEY `IX_PecsCard_boardId` (`boardId`),
    CONSTRAINT `FK_PecsCard_board` FOREIGN KEY (`boardId`) REFERENCES `PecsBoard` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* ============================================================================
   6. MONITOREO (7)
   ============================================================================ */

/* 6.1 StudentProgress — progreso acumulado, 1:1 con el estudiante */
CREATE TABLE `StudentProgress` (
    `id`                   INT           NOT NULL AUTO_INCREMENT,
    `studentId`            INT           NOT NULL,
    `completionPercentage` DECIMAL(5,2)  NULL,
    `currentLevel`         VARCHAR(50)   NULL,
    `strengths`            TEXT          NULL,
    `weaknesses`           TEXT          NULL,
    `recommendation`       TEXT          NULL,
    `totalStudyTime`       INT           NULL,
    `lastSessionAt`        DATETIME      NULL,
    `createdAt`            DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt`            DATETIME      NULL,
    PRIMARY KEY (`id`),
    UNIQUE KEY `UQ_StudentProgress_student` (`studentId`),
    CONSTRAINT `FK_StudentProgress_student` FOREIGN KEY (`studentId`) REFERENCES `Student` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 6.2 StudyHistory — historial de sesiones de estudio registradas por el tutor */
CREATE TABLE `StudyHistory` (
    `id`         INT           NOT NULL AUTO_INCREMENT,
    `studentId`  INT           NOT NULL,
    `subjectId`  INT           NOT NULL,
    `lessonId`   INT           NOT NULL,
    `score`      DECIMAL(5,2)  NULL,
    `studyTime`  INT           NULL,
    `studyDate`  DATE          NULL,
    `result`     VARCHAR(20)   NULL,
    `difficulty` VARCHAR(20)   NULL,
    PRIMARY KEY (`id`),
    KEY `IX_StudyHistory_studentId` (`studentId`),
    KEY `IX_StudyHistory_lessonId` (`lessonId`),
    CONSTRAINT `FK_StudyHistory_student` FOREIGN KEY (`studentId`) REFERENCES `Student` (`id`),
    CONSTRAINT `FK_StudyHistory_subject` FOREIGN KEY (`subjectId`) REFERENCES `Subject` (`id`),
    CONSTRAINT `FK_StudyHistory_lesson`  FOREIGN KEY (`lessonId`)  REFERENCES `Lesson` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 6.3 StudentHabit — indicadores/gustos favoritos ante estrés (tranquilizador
      individual). No confundir con la autorregulación (grupal) */
CREATE TABLE `StudentHabit` (
    `id`           INT           NOT NULL AUTO_INCREMENT,
    `studentId`    INT           NULL,
    `subjectId`    INT           NULL,
    `name`         VARCHAR(50)   NULL,
    `frequency`    VARCHAR(100)  NULL,
    `observations` TEXT          NULL,
    `createdAt`    DATETIME      NULL,
    PRIMARY KEY (`id`),
    KEY `IX_StudentHabit_studentId` (`studentId`),
    CONSTRAINT `FK_StudentHabit_student` FOREIGN KEY (`studentId`) REFERENCES `Student` (`id`),
    CONSTRAINT `FK_StudentHabit_subject` FOREIGN KEY (`subjectId`) REFERENCES `Subject` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 6.4 Routine — rutina cotidiana registrada por el tutor */
CREATE TABLE `Routine` (
    `id`              INT           NOT NULL AUTO_INCREMENT,
    `studentId`       INT           NOT NULL,
    `name`            VARCHAR(120)  NULL,
    `description`     TEXT          NULL,
    `startDate`       DATE          NULL,
    `endDate`         DATE          NULL,
    `status`          VARCHAR(20)   NULL,
    `createdByUserId` INT           NULL,
    `createdAt`       DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    KEY `IX_Routine_studentId` (`studentId`),
    CONSTRAINT `FK_Routine_student`   FOREIGN KEY (`studentId`)       REFERENCES `Student` (`id`),
    CONSTRAINT `FK_Routine_createdBy` FOREIGN KEY (`createdByUserId`) REFERENCES `User` (`id`) ON DELETE SET NULL
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 6.5 HabitCompliance — cumplimiento del hábito; aplica cuando la rutina
      coincide con el periodo de clases */
CREATE TABLE `HabitCompliance` (
    `id`             INT           NOT NULL AUTO_INCREMENT,
    `habitId`        INT           NOT NULL,
    `complianceDate` DATE          NOT NULL,
    `isFulfilled`    TINYINT(1)    NOT NULL DEFAULT 0,
    `observation`    TEXT          NULL,
    `registeredById` INT           NULL,
    `createdAt`      DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    KEY `IX_HabitCompliance_habitId` (`habitId`),
    CONSTRAINT `FK_HabitCompliance_habit` FOREIGN KEY (`habitId`) REFERENCES `StudentHabit` (`id`),
    CONSTRAINT `FK_HabitCompliance_registeredBy` FOREIGN KEY (`registeredById`) REFERENCES `User` (`id`) ON DELETE SET NULL
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 6.6 RoutineDetail — detalle de la rutina */
CREATE TABLE `RoutineDetail` (
    `id`              INT           NOT NULL AUTO_INCREMENT,
    `routineId`       INT           NOT NULL,
    `timeOfDay`       TIME(6)       NULL,
    `activity`        VARCHAR(150)  NULL,
    `description`     TEXT          NULL,
    `durationMinutes` INT           NULL,
    PRIMARY KEY (`id`),
    KEY `IX_RoutineDetail_routineId` (`routineId`),
    CONSTRAINT `FK_RoutineDetail_routine` FOREIGN KEY (`routineId`) REFERENCES `Routine` (`id`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* 6.7 RoutineLog — ejecución diaria de la rutina cotidiana */
CREATE TABLE `RoutineLog` (
    `id`              INT           NOT NULL AUTO_INCREMENT,
    `routineDetailId` INT           NOT NULL,
    `studentId`       INT           NOT NULL,
    `status`          VARCHAR(20)   NULL,
    `observation`     TEXT          NULL,
    `logDate`         DATETIME      NULL,
    `registeredById`  INT           NULL,
    PRIMARY KEY (`id`),
    KEY `IX_RoutineLog_routineDetailId` (`routineDetailId`),
    KEY `IX_RoutineLog_studentId` (`studentId`),
    CONSTRAINT `FK_RoutineLog_detail`  FOREIGN KEY (`routineDetailId`) REFERENCES `RoutineDetail` (`id`),
    CONSTRAINT `FK_RoutineLog_student` FOREIGN KEY (`studentId`)       REFERENCES `Student` (`id`),
    CONSTRAINT `FK_RoutineLog_registeredBy` FOREIGN KEY (`registeredById`) REFERENCES `User` (`id`) ON DELETE SET NULL
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

/* ============================================================================
   7. ÍNDICES AUXILIARES ADICIONALES
   (Las FKs ya crean índices automáticamente en InnoDB; se agregan los de
   columnas de filtrado frecuente.)
   ============================================================================ */
CREATE INDEX `IX_LinkCode_status` ON `LinkCode` (`status`);
CREATE INDEX `IX_LinkCode_issuedById` ON `LinkCode` (`issuedById`);

/* ============================================================================
   8. SEED
   ----------------------------------------------------------------------------
   Catálogos: roles, admin, materias, keywords, grupo.
   Escenario demo (flujo aceptado de fases/FASE_PROTOTIPO.md §5):
     institución crea docente y emite código TEACHER_CONTRACT → docente activa
     y planifica contenido → institución emite código ENROLLMENT al tutor →
     tutor activa, ve la ficha de su hij@ y registra perfil (gustos, hábito,
     rutina) → docente consulta progreso del estudiante.
   Los ids se resuelven con variables de usuario (LAST_INSERT_ID / subconsultas).
   Nota: reemplaza 'HASH_PLACEHOLDER_REEMPLAZAR' por hashes bcrypt reales.
   ============================================================================ */

-- 8.1 Roles (catálogo)
INSERT INTO `Role` (`name`, `description`, `isActive`) VALUES
    ('INSTITUTION', 'Institución educativa: gestiona entidades, grupos, materias y códigos de activación.', 1),
    ('TEACHER',     'Docente: activa su cuenta, planifica contenido y da seguimiento al progreso.',          1),
    ('GUARDIAN',    'Tutor: registra perfil, hábitos y rutinas del estudiante; sigue su avance.',            1);

-- 8.2 Usuario administrador (institución)
INSERT INTO `User` (`email`, `nationalId`, `passwordHash`, `isActive`) VALUES
    ('admin@lumina.edu', '0000000000', 'HASH_PLACEHOLDER_REEMPLAZAR', 1);
SET @adminId = LAST_INSERT_ID();

INSERT INTO `UserRole` (`userId`, `roleId`) VALUES
    (@adminId, (SELECT `id` FROM `Role` WHERE `name` = 'INSTITUTION'));

-- 8.3 Materias (catálogo)
INSERT INTO `Subject` (`name`, `description`, `color`, `icon`) VALUES
    ('Matemáticas',        'Pensamiento lógico-matemático.',          '#3B82F6', 'math'),
    ('Comunicación',       'Comprensión y expresión del lenguaje.',   '#10B981', 'book'),
    ('Ciencia y Ambiente', 'Exploración del entorno natural.',        '#F59E0B', 'science');

-- 8.4 Keywords iniciales (catálogo de la base de conocimientos)
INSERT INTO `Keyword` (`name`) VALUES
    ('número'), ('conteo'), ('animales'), ('colores'), ('rutina'), ('higiene');

-- 8.5 Grupo (catálogo)
INSERT INTO `ClassGroup` (`name`, `gradeLevel`, `description`, `isActive`) VALUES
    ('Aula Inicial 4', 'Inicial 4', 'Grupo de educación inicial.', 1);
SET @groupId = LAST_INSERT_ID();

-- 8.6 Docente (entidad + cuenta + rol)
INSERT INTO `User` (`email`, `nationalId`, `passwordHash`, `isActive`) VALUES
    ('docente@lumina.edu', '0123456789', 'HASH_PLACEHOLDER_REEMPLAZAR', 1);
SET @teacherUserId = LAST_INSERT_ID();

INSERT INTO `UserRole` (`userId`, `roleId`) VALUES
    (@teacherUserId, (SELECT `id` FROM `Role` WHERE `name` = 'TEACHER'));

INSERT INTO `Teacher` (`userId`, `firstName`, `lastName`, `nationalId`, `personalEmail`,
                       `phone`, `specialty`, `degree`, `entityStatus`) VALUES
    (@teacherUserId, 'Carlos', 'Quispe', '0123456789', 'docente@lumina.edu',
     '+51 900 111 222', 'Educación inicial', 'Lic. en Educación', 'ACTIVE');
SET @teacherId = LAST_INSERT_ID();

-- 8.7 Código de activación del docente (contratación): emitido y canjeado
INSERT INTO `LinkCode` (`code`, `purpose`, `status`, `issuedById`, `expiresAt`, `usedById`, `usedAt`) VALUES
    ('LUM-TEACH-2026-0001', 'TEACHER_CONTRACT', 'USED', @adminId, DATE_ADD(NOW(), INTERVAL 30 DAY), @teacherUserId, NOW());

-- 8.8 Tutor (entidad + cuenta + rol)
INSERT INTO `User` (`email`, `nationalId`, `passwordHash`, `isActive`) VALUES
    ('tutor@lumina.edu', '0987654321', 'HASH_PLACEHOLDER_REEMPLAZAR', 1);
SET @guardianUserId = LAST_INSERT_ID();

INSERT INTO `UserRole` (`userId`, `roleId`) VALUES
    (@guardianUserId, (SELECT `id` FROM `Role` WHERE `name` = 'GUARDIAN'));

INSERT INTO `Guardian` (`userId`, `firstName`, `lastName`, `nationalId`, `personalEmail`,
                        `phone`, `relationship`, `entityStatus`) VALUES
    (@guardianUserId, 'María', 'Pérez', '0987654321', 'tutor@lumina.edu',
     '+51 900 333 444', 'mother', 'ACTIVE');
SET @guardianId = LAST_INSERT_ID();

-- 8.9 Estudiante (matrícula estándar por la institución)
INSERT INTO `Student` (`groupId`, `userId`, `firstName`, `lastName`, `uniqueNumber`,
                       `birthDate`, `gender`, `languageLevel`, `clinicalInfo`, `isActive`) VALUES
    (@groupId, NULL, 'Lucas', 'Pérez', 'STU-2026-001',
     '2020-05-14', 'M', 'Nivel 2', 'TEA Nivel 1; intereses en vehículos.', 1);
SET @studentId = LAST_INSERT_ID();

-- 8.10 Código de activación del tutor (matrícula): emitido y canjeado → ve la ficha
INSERT INTO `LinkCode` (`code`, `purpose`, `status`, `issuedById`, `expiresAt`, `usedById`, `usedAt`) VALUES
    ('LUM-ENR-2026-0001', 'ENROLLMENT', 'USED', @adminId, DATE_ADD(NOW(), INTERVAL 30 DAY), @guardianUserId, NOW());

-- 8.11 Relaciones entidad ↔ estudiante
INSERT INTO `EntityStudentRelation` (`entityId`, `entityType`, `studentId`, `relationType`, `isActive`, `assignedAt`) VALUES
    (@guardianId, 'GUARDIAN', @studentId, 'PRIMARY_GUARDIAN', 1, '2026-03-02'),
    (@teacherId,  'TEACHER',  @studentId, 'REFERRING_TEACHER', 1, '2026-03-02');

-- 8.12 Asignación docente + materia al grupo
SET @mathId = (SELECT `id` FROM `Subject` WHERE `name` = 'Matemáticas');
INSERT INTO `GroupSubject` (`groupId`, `subjectId`, `teacherId`, `isActive`, `assignmentDate`) VALUES
    (@groupId, @mathId, @teacherId, 1, '2026-03-02');

-- 8.13 Planificación: módulo → lección → pasos → contenido → keywords
INSERT INTO `Module` (`subjectId`, `name`, `description`) VALUES
    (@mathId, 'Módulo 1: Números del 1 al 10', 'Reconocimiento y conteo de los primeros números.');
SET @moduleId = LAST_INSERT_ID();

INSERT INTO `Lesson` (`moduleId`, `title`, `description`, `type`, `durationMinutes`) VALUES
    (@moduleId, 'Lección 1: El número 1', 'Identificar el número 1 en la vida diaria.', 'guided', 10);
SET @lessonId = LAST_INSERT_ID();

INSERT INTO `LessonStep` (`lessonId`, `stepNumber`, `title`, `description`, `contentType`, `isActive`) VALUES
    (@lessonId, 1, 'Presentación', 'Mostrar el número 1 con objetos del aula.', 'video', 1),
    (@lessonId, 2, 'Práctica',     'Repasar con tarjetas.', 'activity', 1);

INSERT INTO `LearningContent` (`lessonId`, `title`, `description`, `type`, `isDictionary`,
                               `isRoutine`, `isException`, `subjectId`, `level`, `isActive`) VALUES
    (@lessonId, 'Número 1', 'Representación visual y auditiva del número 1.', 'DICTIONARY', 1, 0, 0, @mathId, 'inicial', 1);
SET @contentId = LAST_INSERT_ID();

INSERT INTO `ContentKeyword` (`contentId`, `keywordId`)
SELECT @contentId, `id` FROM `Keyword` WHERE `name` IN ('número', 'conteo');

-- 8.14 PECS: tablero + tarjetas
INSERT INTO `PecsBoard` (`studentId`, `name`, `description`) VALUES
    (@studentId, 'Tablero: Rutina de la mañana', 'Tarjetas para anticipar la rutina de la mañana.');
SET @boardId = LAST_INSERT_ID();

INSERT INTO `PecsCard` (`boardId`, `title`, `category`, `orderNumber`) VALUES
    (@boardId, 'Lavarse los dientes', 'Higiene', 1),
    (@boardId, 'Desayunar', 'Alimentación', 2);

-- 8.15 Perfil del estudiante (tutor): gustos (N registros) y hábito tranquilizador
INSERT INTO `StudentInterest` (`studentId`, `name`, `description`) VALUES
    (@studentId, 'Vehículos', 'Juega con autos y camiones de juguete.'),
    (@studentId, 'Música',    'Canciones infantiles rítmicas.');

INSERT INTO `StudentHabit` (`studentId`, `subjectId`, `name`, `frequency`, `observations`) VALUES
    (@studentId, NULL, 'Pelota antiestrés', 'Cuando se siente sobrecargado en clase.', 'Tranquilizador individual.');
SET @habitId = LAST_INSERT_ID();

-- 8.16 Rutina cotidiana (tutor): rutina + detalle
INSERT INTO `Routine` (`studentId`, `name`, `description`, `startDate`, `status`, `createdByUserId`) VALUES
    (@studentId, 'Rutina de la mañana', 'Actividades antes de ir a la escuela.', '2026-03-02', 'ACTIVE', @guardianUserId);
SET @routineId = LAST_INSERT_ID();

INSERT INTO `RoutineDetail` (`routineId`, `timeOfDay`, `activity`, `description`, `durationMinutes`) VALUES
    (@routineId, '07:30:00', 'Despertar y vestirse', 'Alistar el uniforme.', 15);
SET @wakeUpDetailId = LAST_INSERT_ID();

INSERT INTO `RoutineDetail` (`routineId`, `timeOfDay`, `activity`, `description`, `durationMinutes`) VALUES
    (@routineId, '07:50:00', 'Desayunar', 'Desayuno en familia.', 20);
SET @breakfastDetailId = LAST_INSERT_ID();

-- 8.17 Monitoreo: historial, cumplimiento del hábito, ejecución de rutina y progreso
INSERT INTO `StudyHistory` (`studentId`, `subjectId`, `lessonId`, `score`, `studyTime`, `studyDate`, `result`, `difficulty`) VALUES
    (@studentId, @mathId, @lessonId, 85.00, 10, '2026-03-04', 'COMPLETED', 'MEDIUM');

INSERT INTO `HabitCompliance` (`habitId`, `complianceDate`, `isFulfilled`, `observation`, `registeredById`) VALUES
    (@habitId, '2026-03-04', 1, 'Usó la pelota antiestrés y volvió a la actividad.', @guardianUserId);

INSERT INTO `RoutineLog` (`routineDetailId`, `studentId`, `status`, `observation`, `logDate`, `registeredById`) VALUES
    (@breakfastDetailId, @studentId, 'DONE', 'Completó el desayuno a tiempo.', '2026-03-04 07:55:00', @guardianUserId);

INSERT INTO `StudentProgress` (`studentId`, `completionPercentage`, `currentLevel`, `strengths`,
                               `weaknesses`, `recommendation`, `totalStudyTime`, `lastSessionAt`) VALUES
    (@studentId, 25.00, 'Inicial', 'Reconoce el número 1.', 'Reforzar el conteo.',
     'Seguir con rutinas diarias.', 10, '2026-03-04 08:10:00');

/* ============================================================================
   9. TRIGGER OPCIONAL — RN-01 (máx. 10 estudiantes activos por grupo)
   ----------------------------------------------------------------------------
   Los CHECK no admiten subconsultas; la regla se puede garantizar con un
   trigger de insert/update. Se incluye comentado: si se activa, un grupo no
   podrá superar 10 estudiantes activos.
   ============================================================================
DELIMITER $$
CREATE TRIGGER trg_Student_RN01_CuotaGrupo
BEFORE INSERT ON `Student`
FOR EACH ROW
BEGIN
    IF NEW.`isActive` = 1 AND (
        SELECT COUNT(*)
        FROM `Student`
        WHERE `groupId` = NEW.`groupId`
          AND `isActive` = 1
    ) >= 10 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'RN-01: un grupo no puede tener más de 10 estudiantes activos.';
    END IF;
END$$
DELIMITER ;
*/

SELECT 'LUMINA — Fase Prototipo: DDL y seed completados (27 tablas).' AS Resultado;
