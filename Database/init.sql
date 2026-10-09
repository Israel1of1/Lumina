IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'LUMINA')
BEGIN
    CREATE DATABASE [LUMINA];
END
GO

USE [LUMINA];
GO

/****** Objeto: StoredProcedure [dbo].[USP_UpdateUserPassword] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_UpdateUserPassword]
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateTeacherProfile] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_UpdateTeacherProfile]
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateSubject] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_UpdateSubject]
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateStudent] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_UpdateStudent]
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateRole] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_UpdateRole]
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateModule] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_UpdateModule]
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateLessonStep] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_UpdateLessonStep]
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateLesson] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_UpdateLesson]
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateGuardianProfile] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_UpdateGuardianProfile]
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateClassGroup] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_UpdateClassGroup]
GO
/****** Objeto: StoredProcedure [dbo].[USP_SetStudentActive] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_SetStudentActive]
GO
/****** Objeto: StoredProcedure [dbo].[USP_SetGroupSubjectActive] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_SetGroupSubjectActive]
GO
/****** Objeto: StoredProcedure [dbo].[USP_SetClassGroupActive] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_SetClassGroupActive]
GO
/****** Objeto: StoredProcedure [dbo].[USP_RevokeLinkCode] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_RevokeLinkCode]
GO
/****** Objeto: StoredProcedure [dbo].[USP_RegisterUserWithLinkCode] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_RegisterUserWithLinkCode]
GO
/****** Objeto: StoredProcedure [dbo].[USP_PatchTeacherProfile] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_PatchTeacherProfile]
GO
/****** Objeto: StoredProcedure [dbo].[USP_InsertRole] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_InsertRole]
GO
/****** Objeto: StoredProcedure [dbo].[USP_InsertNewSubject] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_InsertNewSubject]
GO
/****** Objeto: StoredProcedure [dbo].[USP_InsertNewModule] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_InsertNewModule]
GO
/****** Objeto: StoredProcedure [dbo].[USP_InsertNewLessonStep] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_InsertNewLessonStep]
GO
/****** Objeto: StoredProcedure [dbo].[USP_InsertNewLesson] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_InsertNewLesson]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetUserById] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetUserById]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetUserByEmail] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetUserByEmail]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetTeacherByUserId] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetTeacherByUserId]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetSubjectByName] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetSubjectByName]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetSubjectById] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetSubjectById]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetStudentsByGroup] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetStudentsByGroup]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetStudentById] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetStudentById]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetRoleById] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetRoleById]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetRelationsByStudent] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetRelationsByStudent]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetModuleById] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetModuleById]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetLinkCodeInfo] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetLinkCodeInfo]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetLessonStepById] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetLessonStepById]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetLessonById] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetLessonById]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetGuardianByUserId] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetGuardianByUserId]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetGroupSubjectsByTeacher] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetGroupSubjectsByTeacher]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetGroupSubjectsByGroup] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetGroupSubjectsByGroup]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetClassGroupById] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetClassGroupById]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllTeachers] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetAllTeachers]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllSubject] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetAllSubject]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllRole] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetAllRole]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllModule] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetAllModule]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllLessonStep] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetAllLessonStep]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllLesson] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetAllLesson]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllGuardians] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetAllGuardians]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllClassGroups] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetAllClassGroups]
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetActiveStudentsForEntity] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_GetActiveStudentsForEntity]
GO
/****** Objeto: StoredProcedure [dbo].[USP_EndStudentRelation] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_EndStudentRelation]
GO
/****** Objeto: StoredProcedure [dbo].[USP_DeactivateTeacher] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_DeactivateTeacher]
GO
/****** Objeto: StoredProcedure [dbo].[USP_DeactivateGuardian] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_DeactivateGuardian]
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateTeacherLinkCode] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_CreateTeacherLinkCode]
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateStudentRelation] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_CreateStudentRelation]
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateStudent] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_CreateStudent]
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateGuardianLinkCode] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_CreateGuardianLinkCode]
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateGroupSubject] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_CreateGroupSubject]
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateClassGroup] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
DROP PROCEDURE [dbo].[USP_CreateClassGroup]
GO
ALTER TABLE [dbo].[Teacher] DROP CONSTRAINT [CK_Teacher_entityStatus]
GO
ALTER TABLE [dbo].[LinkCode] DROP CONSTRAINT [CK_LinkCode_targetEntityType]
GO
ALTER TABLE [dbo].[LinkCode] DROP CONSTRAINT [CK_LinkCode_status]
GO
ALTER TABLE [dbo].[LinkCode] DROP CONSTRAINT [CK_LinkCode_purpose]
GO
ALTER TABLE [dbo].[LearningContent] DROP CONSTRAINT [CK_LearningContent_type]
GO
ALTER TABLE [dbo].[EntityStudentRelation] DROP CONSTRAINT [CK_EntityStudentRelation_relationType]
GO
ALTER TABLE [dbo].[EntityStudentRelation] DROP CONSTRAINT [CK_EntityStudentRelation_entityType]
GO
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_user]
GO
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_role]
GO
ALTER TABLE [dbo].[Teacher] DROP CONSTRAINT [FK_Teacher_user]
GO
ALTER TABLE [dbo].[StudyHistory] DROP CONSTRAINT [FK_StudyHistory_subject]
GO
ALTER TABLE [dbo].[StudyHistory] DROP CONSTRAINT [FK_StudyHistory_student]
GO
ALTER TABLE [dbo].[StudyHistory] DROP CONSTRAINT [FK_StudyHistory_lesson]
GO
ALTER TABLE [dbo].[StudentProgress] DROP CONSTRAINT [FK_StudentProgress_student]
GO
ALTER TABLE [dbo].[StudentInterest] DROP CONSTRAINT [FK_StudentInterest_student]
GO
ALTER TABLE [dbo].[StudentHabit] DROP CONSTRAINT [FK_StudentHabit_subject]
GO
ALTER TABLE [dbo].[StudentHabit] DROP CONSTRAINT [FK_StudentHabit_student]
GO
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [FK_Student_user]
GO
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [FK_Student_group]
GO
ALTER TABLE [dbo].[RoutineLog] DROP CONSTRAINT [FK_RoutineLog_student]
GO
ALTER TABLE [dbo].[RoutineLog] DROP CONSTRAINT [FK_RoutineLog_registeredBy]
GO
ALTER TABLE [dbo].[RoutineLog] DROP CONSTRAINT [FK_RoutineLog_detail]
GO
ALTER TABLE [dbo].[RoutineDetail] DROP CONSTRAINT [FK_RoutineDetail_routine]
GO
ALTER TABLE [dbo].[Routine] DROP CONSTRAINT [FK_Routine_student]
GO
ALTER TABLE [dbo].[Routine] DROP CONSTRAINT [FK_Routine_createdBy]
GO
ALTER TABLE [dbo].[PecsCard] DROP CONSTRAINT [FK_PecsCard_board]
GO
ALTER TABLE [dbo].[PecsBoard] DROP CONSTRAINT [FK_PecsBoard_student]
GO
ALTER TABLE [dbo].[Module] DROP CONSTRAINT [FK_Module_subject]
GO
ALTER TABLE [dbo].[LinkCode] DROP CONSTRAINT [FK_LinkCode_usedBy]
GO
ALTER TABLE [dbo].[LinkCode] DROP CONSTRAINT [FK_LinkCode_issuedBy]
GO
ALTER TABLE [dbo].[LessonStep] DROP CONSTRAINT [FK_LessonStep_lesson]
GO
ALTER TABLE [dbo].[Lesson] DROP CONSTRAINT [FK_Lesson_module]
GO
ALTER TABLE [dbo].[LearningContent] DROP CONSTRAINT [FK_LearningContent_subject]
GO
ALTER TABLE [dbo].[LearningContent] DROP CONSTRAINT [FK_LearningContent_lesson]
GO
ALTER TABLE [dbo].[HabitCompliance] DROP CONSTRAINT [FK_HabitCompliance_registeredBy]
GO
ALTER TABLE [dbo].[HabitCompliance] DROP CONSTRAINT [FK_HabitCompliance_habit]
GO
ALTER TABLE [dbo].[Guardian] DROP CONSTRAINT [FK_Guardian_user]
GO
ALTER TABLE [dbo].[GroupSubject] DROP CONSTRAINT [FK_GroupSubject_teacher]
GO
ALTER TABLE [dbo].[GroupSubject] DROP CONSTRAINT [FK_GroupSubject_subject]
GO
ALTER TABLE [dbo].[GroupSubject] DROP CONSTRAINT [FK_GroupSubject_group]
GO
ALTER TABLE [dbo].[EntityStudentRelation] DROP CONSTRAINT [FK_EntityStudentRelation_student]
GO
ALTER TABLE [dbo].[ContentKeyword] DROP CONSTRAINT [FK_ContentKeyword_keyword]
GO
ALTER TABLE [dbo].[ContentKeyword] DROP CONSTRAINT [FK_ContentKeyword_content]
GO
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [DF_UserRole_assignedAt]
GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [DF_User_createdAt]
GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [DF_User_isActive]
GO
ALTER TABLE [dbo].[Teacher] DROP CONSTRAINT [DF_Teacher_createdAt]
GO
ALTER TABLE [dbo].[Teacher] DROP CONSTRAINT [DF_Teacher_entityStatus]
GO
ALTER TABLE [dbo].[Subject] DROP CONSTRAINT [DF_Subject_createdAt]
GO
ALTER TABLE [dbo].[StudentProgress] DROP CONSTRAINT [DF_StudentProgress_createdAt]
GO
ALTER TABLE [dbo].[StudentInterest] DROP CONSTRAINT [DF_StudentInterest_createdAt]
GO
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [DF_Student_createdAt]
GO
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [DF_Student_isActive]
GO
ALTER TABLE [dbo].[Routine] DROP CONSTRAINT [DF_Routine_createdAt]
GO
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Role_createdAt]
GO
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [DF_Role_isActive]
GO
ALTER TABLE [dbo].[PecsCard] DROP CONSTRAINT [DF_PecsCard_createdAt]
GO
ALTER TABLE [dbo].[PecsBoard] DROP CONSTRAINT [DF_PecsBoard_createdAt]
GO
ALTER TABLE [dbo].[Module] DROP CONSTRAINT [DF_Module_createdAt]
GO
ALTER TABLE [dbo].[LinkCode] DROP CONSTRAINT [DF_LinkCode_createdAt]
GO
ALTER TABLE [dbo].[LinkCode] DROP CONSTRAINT [DF_LinkCode_status]
GO
ALTER TABLE [dbo].[LessonStep] DROP CONSTRAINT [DF_LessonStep_createdAt]
GO
ALTER TABLE [dbo].[LessonStep] DROP CONSTRAINT [DF_LessonStep_isActive]
GO
ALTER TABLE [dbo].[Lesson] DROP CONSTRAINT [DF_Lesson_createdAt]
GO
ALTER TABLE [dbo].[LearningContent] DROP CONSTRAINT [DF_LearningContent_createdAt]
GO
ALTER TABLE [dbo].[LearningContent] DROP CONSTRAINT [DF_LearningContent_isActive]
GO
ALTER TABLE [dbo].[LearningContent] DROP CONSTRAINT [DF_LearningContent_isException]
GO
ALTER TABLE [dbo].[LearningContent] DROP CONSTRAINT [DF_LearningContent_isRoutine]
GO
ALTER TABLE [dbo].[LearningContent] DROP CONSTRAINT [DF_LearningContent_isDictionary]
GO
ALTER TABLE [dbo].[Keyword] DROP CONSTRAINT [DF_Keyword_createdAt]
GO
ALTER TABLE [dbo].[HabitCompliance] DROP CONSTRAINT [DF_HabitCompliance_createdAt]
GO
ALTER TABLE [dbo].[HabitCompliance] DROP CONSTRAINT [DF_HabitCompliance_isFulfilled]
GO
ALTER TABLE [dbo].[Guardian] DROP CONSTRAINT [DF_Guardian_createdAt]
GO
ALTER TABLE [dbo].[Guardian] DROP CONSTRAINT [DF_Guardian_entityStatus]
GO
ALTER TABLE [dbo].[GroupSubject] DROP CONSTRAINT [DF_GroupSubject_createdAt]
GO
ALTER TABLE [dbo].[GroupSubject] DROP CONSTRAINT [DF_GroupSubject_isActive]
GO
ALTER TABLE [dbo].[EntityStudentRelation] DROP CONSTRAINT [DF_EntityStudentRelation_createdAt]
GO
ALTER TABLE [dbo].[EntityStudentRelation] DROP CONSTRAINT [DF_EntityStudentRelation_isActive]
GO
ALTER TABLE [dbo].[ClassGroup] DROP CONSTRAINT [DF_ClassGroup_createdAt]
GO
ALTER TABLE [dbo].[ClassGroup] DROP CONSTRAINT [DF_ClassGroup_isActive]
GO
/****** Objeto: Index [UQ_UserRole_user_role] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [UQ_UserRole_user_role]
GO
/****** Objeto: Index [UQ_User_email] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
ALTER TABLE [dbo].[User] DROP CONSTRAINT [UQ_User_email]
GO
/****** Objeto: Index [UQ_Teacher_nationalId] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
ALTER TABLE [dbo].[Teacher] DROP CONSTRAINT [UQ_Teacher_nationalId]
GO
/****** Objeto: Index [UQ_StudentProgress_student] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
ALTER TABLE [dbo].[StudentProgress] DROP CONSTRAINT [UQ_StudentProgress_student]
GO
/****** Objeto: Index [UQ_Student_uniqueNumber] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [UQ_Student_uniqueNumber]
GO
/****** Objeto: Index [UQ_Role_name] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [UQ_Role_name]
GO
/****** Objeto: Index [UQ_LinkCode_code] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
ALTER TABLE [dbo].[LinkCode] DROP CONSTRAINT [UQ_LinkCode_code]
GO
/****** Objeto: Index [UQ_Keyword_name] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
ALTER TABLE [dbo].[Keyword] DROP CONSTRAINT [UQ_Keyword_name]
GO
/****** Objeto: Index [UQ_Guardian_nationalId] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
ALTER TABLE [dbo].[Guardian] DROP CONSTRAINT [UQ_Guardian_nationalId]
GO
/****** Objeto: Index [UQ_GroupSubject] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
ALTER TABLE [dbo].[GroupSubject] DROP CONSTRAINT [UQ_GroupSubject]
GO
/****** Objeto: Index [UQ_ContentKeyword] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
ALTER TABLE [dbo].[ContentKeyword] DROP CONSTRAINT [UQ_ContentKeyword]
GO
/****** Objeto: Table [dbo].[UserRole] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
DROP TABLE [dbo].[UserRole]
GO
/****** Objeto: Table [dbo].[User] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Objeto: Table [dbo].[Teacher] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teacher]') AND type in (N'U'))
DROP TABLE [dbo].[Teacher]
GO
/****** Objeto: Table [dbo].[Subject] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subject]') AND type in (N'U'))
DROP TABLE [dbo].[Subject]
GO
/****** Objeto: Table [dbo].[StudyHistory] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudyHistory]') AND type in (N'U'))
DROP TABLE [dbo].[StudyHistory]
GO
/****** Objeto: Table [dbo].[StudentProgress] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentProgress]') AND type in (N'U'))
DROP TABLE [dbo].[StudentProgress]
GO
/****** Objeto: Table [dbo].[StudentInterest] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentInterest]') AND type in (N'U'))
DROP TABLE [dbo].[StudentInterest]
GO
/****** Objeto: Table [dbo].[StudentHabit] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentHabit]') AND type in (N'U'))
DROP TABLE [dbo].[StudentHabit]
GO
/****** Objeto: Table [dbo].[Student] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
DROP TABLE [dbo].[Student]
GO
/****** Objeto: Table [dbo].[RoutineLog] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoutineLog]') AND type in (N'U'))
DROP TABLE [dbo].[RoutineLog]
GO
/****** Objeto: Table [dbo].[RoutineDetail] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoutineDetail]') AND type in (N'U'))
DROP TABLE [dbo].[RoutineDetail]
GO
/****** Objeto: Table [dbo].[Routine] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Routine]') AND type in (N'U'))
DROP TABLE [dbo].[Routine]
GO
/****** Objeto: Table [dbo].[Role] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Objeto: Table [dbo].[PecsCard] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PecsCard]') AND type in (N'U'))
DROP TABLE [dbo].[PecsCard]
GO
/****** Objeto: Table [dbo].[PecsBoard] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PecsBoard]') AND type in (N'U'))
DROP TABLE [dbo].[PecsBoard]
GO
/****** Objeto: Table [dbo].[Module] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Module]') AND type in (N'U'))
DROP TABLE [dbo].[Module]
GO
/****** Objeto: Table [dbo].[LinkCode] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LinkCode]') AND type in (N'U'))
DROP TABLE [dbo].[LinkCode]
GO
/****** Objeto: Table [dbo].[LessonStep] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LessonStep]') AND type in (N'U'))
DROP TABLE [dbo].[LessonStep]
GO
/****** Objeto: Table [dbo].[Lesson] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lesson]') AND type in (N'U'))
DROP TABLE [dbo].[Lesson]
GO
/****** Objeto: Table [dbo].[LearningContent] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LearningContent]') AND type in (N'U'))
DROP TABLE [dbo].[LearningContent]
GO
/****** Objeto: Table [dbo].[Keyword] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Keyword]') AND type in (N'U'))
DROP TABLE [dbo].[Keyword]
GO
/****** Objeto: Table [dbo].[HabitCompliance] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HabitCompliance]') AND type in (N'U'))
DROP TABLE [dbo].[HabitCompliance]
GO
/****** Objeto: Table [dbo].[Guardian] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Guardian]') AND type in (N'U'))
DROP TABLE [dbo].[Guardian]
GO
/****** Objeto: Table [dbo].[GroupSubject] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupSubject]') AND type in (N'U'))
DROP TABLE [dbo].[GroupSubject]
GO
/****** Objeto: Table [dbo].[EntityStudentRelation] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityStudentRelation]') AND type in (N'U'))
DROP TABLE [dbo].[EntityStudentRelation]
GO
/****** Objeto: Table [dbo].[ContentKeyword] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentKeyword]') AND type in (N'U'))
DROP TABLE [dbo].[ContentKeyword]
GO
/****** Objeto: Table [dbo].[ClassGroup] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClassGroup]') AND type in (N'U'))
DROP TABLE [dbo].[ClassGroup]
GO
/****** Objeto: Table [dbo].[ClassGroup] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassGroup](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](120) NOT NULL,
	[gradeLevel] [varchar](50) NULL,
	[description] [varchar](max) NULL,
	[isActive] [bit] NOT NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_ClassGroup] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[ContentKeyword] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContentKeyword](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contentId] [int] NOT NULL,
	[keywordId] [int] NOT NULL,
 CONSTRAINT [PK_ContentKeyword] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[EntityStudentRelation] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityStudentRelation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[entityId] [int] NOT NULL,
	[entityType] [varchar](20) NOT NULL,
	[studentId] [int] NOT NULL,
	[relationType] [varchar](20) NOT NULL,
	[isActive] [bit] NOT NULL,
	[assignedAt] [date] NULL,
	[endDate] [date] NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_EntityStudentRelation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[GroupSubject] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupSubject](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[groupId] [int] NOT NULL,
	[subjectId] [int] NOT NULL,
	[teacherId] [int] NOT NULL,
	[isActive] [bit] NOT NULL,
	[assignmentDate] [date] NULL,
	[endDate] [date] NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupSubject] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Guardian] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guardian](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[firstName] [varchar](100) NOT NULL,
	[lastName] [varchar](100) NULL,
	[nationalId] [varchar](16) NULL,
	[personalEmail] [varchar](120) NULL,
	[phone] [varchar](20) NULL,
	[address] [varchar](200) NULL,
	[city] [varchar](100) NULL,
	[photo] [varchar](255) NULL,
	[relationship] [varchar](50) NULL,
	[entityStatus] [varchar](20) NOT NULL,
	[dismissalDate] [date] NULL,
	[dismissalReason] [varchar](max) NULL,
	[createdAt] [datetime] NOT NULL,
	[updatedAt] [datetime] NULL,
 CONSTRAINT [PK_Guardian] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[HabitCompliance] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HabitCompliance](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[habitId] [int] NOT NULL,
	[complianceDate] [date] NOT NULL,
	[isFulfilled] [bit] NOT NULL,
	[observation] [varchar](max) NULL,
	[registeredById] [int] NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_HabitCompliance] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Keyword] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Keyword](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Keyword] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[LearningContent] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LearningContent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[lessonId] [int] NOT NULL,
	[title] [varchar](150) NULL,
	[description] [varchar](max) NULL,
	[type] [varchar](20) NULL,
	[isDictionary] [bit] NOT NULL,
	[isRoutine] [bit] NOT NULL,
	[isException] [bit] NOT NULL,
	[subjectId] [int] NULL,
	[level] [varchar](20) NULL,
	[isActive] [bit] NOT NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_LearningContent] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Lesson] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lesson](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[moduleId] [int] NOT NULL,
	[title] [varchar](150) NOT NULL,
	[description] [varchar](max) NULL,
	[type] [varchar](20) NULL,
	[durationMinutes] [int] NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Lesson] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[LessonStep] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LessonStep](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[lessonId] [int] NOT NULL,
	[stepNumber] [int] NULL,
	[title] [varchar](150) NULL,
	[description] [varchar](max) NULL,
	[contentType] [varchar](20) NULL,
	[contentUrl] [varchar](255) NULL,
	[isActive] [bit] NOT NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_LessonStep] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[LinkCode] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkCode](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](50) NOT NULL,
	[purpose] [varchar](20) NOT NULL,
	[status] [varchar](20) NOT NULL,
	[issuedById] [int] NULL,
	[expiresAt] [datetime] NULL,
	[usedById] [int] NULL,
	[usedAt] [datetime] NULL,
	[createdAt] [datetime] NOT NULL,
	[updatedAt] [datetime] NULL,
	[targetEntityType] [varchar](20) NULL,
	[targetEntityId] [int] NULL,
 CONSTRAINT [PK_LinkCode] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Module] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Module](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[subjectId] [int] NOT NULL,
	[name] [varchar](120) NOT NULL,
	[description] [varchar](max) NULL,
	[iconUrl] [varchar](255) NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[PecsBoard] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PecsBoard](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentId] [int] NOT NULL,
	[name] [varchar](100) NULL,
	[description] [varchar](max) NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_PecsBoard] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[PecsCard] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PecsCard](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[boardId] [int] NOT NULL,
	[title] [varchar](100) NULL,
	[imageUrl] [varchar](255) NULL,
	[audioUrl] [varchar](255) NULL,
	[category] [varchar](100) NULL,
	[orderNumber] [int] NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_PecsCard] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Role] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](150) NULL,
	[isActive] [bit] NOT NULL,
	[createdAt] [datetime] NOT NULL,
	[updatedAt] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Routine] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Routine](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentId] [int] NOT NULL,
	[name] [varchar](120) NULL,
	[description] [varchar](max) NULL,
	[startDate] [date] NULL,
	[endDate] [date] NULL,
	[status] [varchar](20) NULL,
	[createdByUserId] [int] NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Routine] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[RoutineDetail] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoutineDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[routineId] [int] NOT NULL,
	[timeOfDay] [time](7) NULL,
	[activity] [varchar](150) NULL,
	[description] [varchar](max) NULL,
	[durationMinutes] [int] NULL,
 CONSTRAINT [PK_RoutineDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[RoutineLog] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoutineLog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[routineDetailId] [int] NOT NULL,
	[studentId] [int] NOT NULL,
	[status] [varchar](20) NULL,
	[observation] [varchar](max) NULL,
	[logDate] [datetime] NULL,
	[registeredById] [int] NULL,
 CONSTRAINT [PK_RoutineLog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Student] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[groupId] [int] NOT NULL,
	[userId] [int] NULL,
	[firstName] [varchar](100) NOT NULL,
	[lastName] [varchar](100) NULL,
	[uniqueNumber] [varchar](30) NULL,
	[birthDate] [date] NULL,
	[gender] [varchar](10) NULL,
	[languageLevel] [varchar](100) NULL,
	[clinicalInfo] [varchar](max) NULL,
	[observations] [varchar](max) NULL,
	[isActive] [bit] NOT NULL,
	[createdAt] [datetime] NOT NULL,
	[updatedAt] [datetime] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[StudentHabit] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentHabit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentId] [int] NULL,
	[subjectId] [int] NULL,
	[name] [varchar](50) NULL,
	[frequency] [varchar](100) NULL,
	[observations] [varchar](max) NULL,
	[createdAt] [datetime] NULL,
 CONSTRAINT [PK_StudentHabit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[StudentInterest] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentInterest](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentId] [int] NOT NULL,
	[name] [varchar](100) NULL,
	[description] [varchar](max) NULL,
	[createdAt] [datetime] NOT NULL,
	[updatedAt] [datetime] NULL,
 CONSTRAINT [PK_StudentInterest] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[StudentProgress] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentProgress](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentId] [int] NOT NULL,
	[completionPercentage] [decimal](5, 2) NULL,
	[currentLevel] [varchar](50) NULL,
	[strengths] [varchar](max) NULL,
	[weaknesses] [varchar](max) NULL,
	[recommendation] [varchar](max) NULL,
	[totalStudyTime] [int] NULL,
	[lastSessionAt] [datetime] NULL,
	[createdAt] [datetime] NOT NULL,
	[updatedAt] [datetime] NULL,
 CONSTRAINT [PK_StudentProgress] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[StudyHistory] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentId] [int] NOT NULL,
	[subjectId] [int] NOT NULL,
	[lessonId] [int] NOT NULL,
	[score] [decimal](5, 2) NULL,
	[studyTime] [int] NULL,
	[studyDate] [date] NULL,
	[result] [varchar](20) NULL,
	[difficulty] [varchar](20) NULL,
 CONSTRAINT [PK_StudyHistory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Subject] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[description] [varchar](max) NULL,
	[color] [varchar](20) NULL,
	[icon] [varchar](100) NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Teacher] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[firstName] [varchar](100) NOT NULL,
	[lastName] [varchar](100) NOT NULL,
	[nationalId] [varchar](16) NULL,
	[personalEmail] [varchar](120) NULL,
	[phone] [varchar](20) NULL,
	[address] [varchar](200) NULL,
	[city] [varchar](100) NULL,
	[photo] [varchar](255) NULL,
	[specialty] [varchar](100) NULL,
	[degree] [varchar](100) NULL,
	[entityStatus] [varchar](20) NOT NULL,
	[dismissalDate] [date] NULL,
	[dismissalReason] [varchar](max) NULL,
	[createdAt] [datetime] NOT NULL,
	[updatedAt] [datetime] NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[User] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](120) NOT NULL,
	[nationalId] [varchar](16) NULL,
	[passwordHash] [varchar](255) NOT NULL,
	[isActive] [bit] NOT NULL,
	[lastLoginAt] [datetime] NULL,
	[createdAt] [datetime] NOT NULL,
	[updatedAt] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[UserRole] Fecha de script: 05/09/2026 07:50:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[roleId] [int] NOT NULL,
	[assignedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ClassGroup] ON 

INSERT [dbo].[ClassGroup] ([id], [name], [gradeLevel], [description], [isActive], [createdAt]) VALUES (1, N'Grupo 1', N'6to A', N'niños de 10 a 12 años', 1, CAST(N'2026-09-04T23:07:49.593' AS DateTime))
SET IDENTITY_INSERT [dbo].[ClassGroup] OFF
GO
SET IDENTITY_INSERT [dbo].[EntityStudentRelation] ON 

INSERT [dbo].[EntityStudentRelation] ([id], [entityId], [entityType], [studentId], [relationType], [isActive], [assignedAt], [endDate], [createdAt]) VALUES (1, 1, N'TEACHER', 1, N'REFERRING_TEACHER', 1, CAST(N'2026-09-05' AS Date), NULL, CAST(N'2026-09-05T14:13:17.140' AS DateTime))
INSERT [dbo].[EntityStudentRelation] ([id], [entityId], [entityType], [studentId], [relationType], [isActive], [assignedAt], [endDate], [createdAt]) VALUES (2, 1, N'GUARDIAN', 1, N'PRIMARY_GUARDIAN', 1, CAST(N'2026-09-05' AS Date), NULL, CAST(N'2026-09-05T14:17:26.913' AS DateTime))
SET IDENTITY_INSERT [dbo].[EntityStudentRelation] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupSubject] ON 

INSERT [dbo].[GroupSubject] ([id], [groupId], [subjectId], [teacherId], [isActive], [assignmentDate], [endDate], [createdAt]) VALUES (1, 1, 1, 1, 1, CAST(N'2026-09-05' AS Date), NULL, CAST(N'2026-09-05T14:15:00.020' AS DateTime))
SET IDENTITY_INSERT [dbo].[GroupSubject] OFF
GO
SET IDENTITY_INSERT [dbo].[Guardian] ON 

INSERT [dbo].[Guardian] ([id], [userId], [firstName], [lastName], [nationalId], [personalEmail], [phone], [address], [city], [photo], [relationship], [entityStatus], [dismissalDate], [dismissalReason], [createdAt], [updatedAt]) VALUES (1, 2, N'Ramon', N'Villavicencio', N'Nicaraguense', N'GuardianRamon@gmail.com', N'00000000', N'Del centro de salud 1/2 C al sur', N'San Marcos', N'...', N'Padre', N'ACTIVE', NULL, NULL, CAST(N'2026-08-27T20:21:49.690' AS DateTime), CAST(N'2026-08-30T22:17:40.890' AS DateTime))
SET IDENTITY_INSERT [dbo].[Guardian] OFF
GO
SET IDENTITY_INSERT [dbo].[LinkCode] ON 

INSERT [dbo].[LinkCode] ([id], [code], [purpose], [status], [issuedById], [expiresAt], [usedById], [usedAt], [createdAt], [updatedAt], [targetEntityType], [targetEntityId]) VALUES (1, N'AA3A19283F', N'TEACHER_CONTRACT', N'REVOKED', NULL, CAST(N'2026-08-28T01:44:15.547' AS DateTime), NULL, NULL, CAST(N'2026-08-27T19:44:30.890' AS DateTime), CAST(N'2026-08-27T19:58:34.880' AS DateTime), N'TEACHER', 1)
INSERT [dbo].[LinkCode] ([id], [code], [purpose], [status], [issuedById], [expiresAt], [usedById], [usedAt], [createdAt], [updatedAt], [targetEntityType], [targetEntityId]) VALUES (2, N'1D497A0314', N'TEACHER_CONTRACT', N'USED', NULL, CAST(N'2030-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2026-08-27T20:03:58.090' AS DateTime), CAST(N'2026-08-27T19:59:01.120' AS DateTime), CAST(N'2026-08-27T20:03:58.090' AS DateTime), N'TEACHER', 1)
INSERT [dbo].[LinkCode] ([id], [code], [purpose], [status], [issuedById], [expiresAt], [usedById], [usedAt], [createdAt], [updatedAt], [targetEntityType], [targetEntityId]) VALUES (3, N'041287D36D', N'ENROLLMENT', N'USED', NULL, CAST(N'2030-01-01T00:00:00.000' AS DateTime), 2, CAST(N'2026-08-27T20:25:57.820' AS DateTime), CAST(N'2026-08-27T20:22:34.833' AS DateTime), CAST(N'2026-08-27T20:25:57.820' AS DateTime), N'GUARDIAN', 1)
SET IDENTITY_INSERT [dbo].[LinkCode] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([id], [name], [description], [isActive], [createdAt], [updatedAt]) VALUES (1, N'INSTITUCION', N'Centro educativo administrador de la plataforma', 1, CAST(N'2026-08-27T01:06:50.260' AS DateTime), NULL)
INSERT [dbo].[Role] ([id], [name], [description], [isActive], [createdAt], [updatedAt]) VALUES (2, N'DOCENTE', N'Docente que planifica e imparte clase', 1, CAST(N'2026-08-27T01:06:50.267' AS DateTime), CAST(N'2026-08-27T20:31:14.083' AS DateTime))
INSERT [dbo].[Role] ([id], [name], [description], [isActive], [createdAt], [updatedAt]) VALUES (3, N'TUTOR', N'Responsable del estudiante', 1, CAST(N'2026-08-27T01:06:50.267' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([id], [groupId], [userId], [firstName], [lastName], [uniqueNumber], [birthDate], [gender], [languageLevel], [clinicalInfo], [observations], [isActive], [createdAt], [updatedAt]) VALUES (1, 1, NULL, N'Thiago', N'Lopez', N'001', CAST(N'2015-09-05' AS Date), N'Masculino', N'basico', N'Es alergico a la miel', N'es muy observador', 1, CAST(N'2026-09-04T23:10:37.160' AS DateTime), CAST(N'2026-09-04T23:14:57.027' AS DateTime))
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([id], [name], [description], [color], [icon], [createdAt]) VALUES (1, N'Ciencias Naturales', N'Flora y fauna', N'Verde', N'https://www.magnific.com/es/vectores/ciencias-naturales-sociales-dibujo/2', CAST(N'2026-09-05T13:46:57.073' AS DateTime))
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([id], [userId], [firstName], [lastName], [nationalId], [personalEmail], [phone], [address], [city], [photo], [specialty], [degree], [entityStatus], [dismissalDate], [dismissalReason], [createdAt], [updatedAt]) VALUES (1, 1, N'Ana', N'Perez', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'ACTIVE', NULL, NULL, CAST(N'2026-08-27T19:26:58.783' AS DateTime), CAST(N'2026-08-27T20:03:58.090' AS DateTime))
SET IDENTITY_INSERT [dbo].[Teacher] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id], [email], [nationalId], [passwordHash], [isActive], [lastLoginAt], [createdAt], [updatedAt]) VALUES (1, N'TeacherAna@gmail.com', NULL, N'$2a$11$EMnDsMWXIF75.emPHQHi7ut2K6H8OiB58.MeExSUL/xGKGwU1ucJi', 1, NULL, CAST(N'2026-08-27T20:03:58.087' AS DateTime), NULL)
INSERT [dbo].[User] ([id], [email], [nationalId], [passwordHash], [isActive], [lastLoginAt], [createdAt], [updatedAt]) VALUES (2, N'GuardianRamon@gmail.com', NULL, N'$2a$11$PZBJSMZOHZKzHb1VohBOzuJ0gQrx8BSLWo624fzbtLRmjlBDsgQs2', 1, NULL, CAST(N'2026-08-27T20:25:57.817' AS DateTime), NULL)
INSERT [dbo].[User] ([id], [email], [nationalId], [passwordHash], [isActive], [lastLoginAt], [createdAt], [updatedAt]) VALUES (3, N'admin@lumina.edu', NULL, N'$2a$11$bnFzjAFhz33OB5cCKCfNlu9PVSsYlKgJWpS2obeavOgjpHm4umdSS', 1, NULL, CAST(N'2026-09-05T12:56:06.970' AS DateTime), CAST(N'2026-09-05T13:30:47.780' AS DateTime))
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([id], [userId], [roleId], [assignedAt]) VALUES (1, 1, 2, CAST(N'2026-08-27T20:03:58.090' AS DateTime))
INSERT [dbo].[UserRole] ([id], [userId], [roleId], [assignedAt]) VALUES (2, 2, 3, CAST(N'2026-08-27T20:25:57.817' AS DateTime))
INSERT [dbo].[UserRole] ([id], [userId], [roleId], [assignedAt]) VALUES (3, 1, 1, CAST(N'2026-08-28T01:38:38.420' AS DateTime))
INSERT [dbo].[UserRole] ([id], [userId], [roleId], [assignedAt]) VALUES (4, 3, 1, CAST(N'2026-09-05T12:56:06.973' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
/****** Objeto: Index [UQ_ContentKeyword] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
ALTER TABLE [dbo].[ContentKeyword] ADD  CONSTRAINT [UQ_ContentKeyword] UNIQUE NONCLUSTERED 
(
	[contentId] ASC,
	[keywordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Objeto: Index [UQ_GroupSubject] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
ALTER TABLE [dbo].[GroupSubject] ADD  CONSTRAINT [UQ_GroupSubject] UNIQUE NONCLUSTERED 
(
	[groupId] ASC,
	[subjectId] ASC,
	[teacherId] ASC,
	[assignmentDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Guardian_nationalId] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
ALTER TABLE [dbo].[Guardian] ADD  CONSTRAINT [UQ_Guardian_nationalId] UNIQUE NONCLUSTERED 
(
	[nationalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Keyword_name] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
ALTER TABLE [dbo].[Keyword] ADD  CONSTRAINT [UQ_Keyword_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_LinkCode_code] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
ALTER TABLE [dbo].[LinkCode] ADD  CONSTRAINT [UQ_LinkCode_code] UNIQUE NONCLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Role_name] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [UQ_Role_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Student_uniqueNumber] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [UQ_Student_uniqueNumber] UNIQUE NONCLUSTERED 
(
	[uniqueNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Objeto: Index [UQ_StudentProgress_student] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
ALTER TABLE [dbo].[StudentProgress] ADD  CONSTRAINT [UQ_StudentProgress_student] UNIQUE NONCLUSTERED 
(
	[studentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Teacher_nationalId] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [UQ_Teacher_nationalId] UNIQUE NONCLUSTERED 
(
	[nationalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_User_email] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UQ_User_email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Objeto: Index [UQ_UserRole_user_role] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [UQ_UserRole_user_role] UNIQUE NONCLUSTERED 
(
	[userId] ASC,
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClassGroup] ADD  CONSTRAINT [DF_ClassGroup_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[ClassGroup] ADD  CONSTRAINT [DF_ClassGroup_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[EntityStudentRelation] ADD  CONSTRAINT [DF_EntityStudentRelation_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[EntityStudentRelation] ADD  CONSTRAINT [DF_EntityStudentRelation_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[GroupSubject] ADD  CONSTRAINT [DF_GroupSubject_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[GroupSubject] ADD  CONSTRAINT [DF_GroupSubject_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Guardian] ADD  CONSTRAINT [DF_Guardian_entityStatus]  DEFAULT ('ACTIVE') FOR [entityStatus]
GO
ALTER TABLE [dbo].[Guardian] ADD  CONSTRAINT [DF_Guardian_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[HabitCompliance] ADD  CONSTRAINT [DF_HabitCompliance_isFulfilled]  DEFAULT ((0)) FOR [isFulfilled]
GO
ALTER TABLE [dbo].[HabitCompliance] ADD  CONSTRAINT [DF_HabitCompliance_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Keyword] ADD  CONSTRAINT [DF_Keyword_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[LearningContent] ADD  CONSTRAINT [DF_LearningContent_isDictionary]  DEFAULT ((0)) FOR [isDictionary]
GO
ALTER TABLE [dbo].[LearningContent] ADD  CONSTRAINT [DF_LearningContent_isRoutine]  DEFAULT ((0)) FOR [isRoutine]
GO
ALTER TABLE [dbo].[LearningContent] ADD  CONSTRAINT [DF_LearningContent_isException]  DEFAULT ((0)) FOR [isException]
GO
ALTER TABLE [dbo].[LearningContent] ADD  CONSTRAINT [DF_LearningContent_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[LearningContent] ADD  CONSTRAINT [DF_LearningContent_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Lesson] ADD  CONSTRAINT [DF_Lesson_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[LessonStep] ADD  CONSTRAINT [DF_LessonStep_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[LessonStep] ADD  CONSTRAINT [DF_LessonStep_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[LinkCode] ADD  CONSTRAINT [DF_LinkCode_status]  DEFAULT ('PENDING') FOR [status]
GO
ALTER TABLE [dbo].[LinkCode] ADD  CONSTRAINT [DF_LinkCode_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[PecsBoard] ADD  CONSTRAINT [DF_PecsBoard_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[PecsCard] ADD  CONSTRAINT [DF_PecsCard_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Routine] ADD  CONSTRAINT [DF_Routine_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF_Student_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF_Student_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[StudentInterest] ADD  CONSTRAINT [DF_StudentInterest_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[StudentProgress] ADD  CONSTRAINT [DF_StudentProgress_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Subject] ADD  CONSTRAINT [DF_Subject_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [DF_Teacher_entityStatus]  DEFAULT ('ACTIVE') FOR [entityStatus]
GO
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [DF_Teacher_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_createdAt]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [DF_UserRole_assignedAt]  DEFAULT (getdate()) FOR [assignedAt]
GO
ALTER TABLE [dbo].[ContentKeyword]  WITH CHECK ADD  CONSTRAINT [FK_ContentKeyword_content] FOREIGN KEY([contentId])
REFERENCES [dbo].[LearningContent] ([id])
GO
ALTER TABLE [dbo].[ContentKeyword] CHECK CONSTRAINT [FK_ContentKeyword_content]
GO
ALTER TABLE [dbo].[ContentKeyword]  WITH CHECK ADD  CONSTRAINT [FK_ContentKeyword_keyword] FOREIGN KEY([keywordId])
REFERENCES [dbo].[Keyword] ([id])
GO
ALTER TABLE [dbo].[ContentKeyword] CHECK CONSTRAINT [FK_ContentKeyword_keyword]
GO
ALTER TABLE [dbo].[EntityStudentRelation]  WITH CHECK ADD  CONSTRAINT [FK_EntityStudentRelation_student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[EntityStudentRelation] CHECK CONSTRAINT [FK_EntityStudentRelation_student]
GO
ALTER TABLE [dbo].[GroupSubject]  WITH CHECK ADD  CONSTRAINT [FK_GroupSubject_group] FOREIGN KEY([groupId])
REFERENCES [dbo].[ClassGroup] ([id])
GO
ALTER TABLE [dbo].[GroupSubject] CHECK CONSTRAINT [FK_GroupSubject_group]
GO
ALTER TABLE [dbo].[GroupSubject]  WITH CHECK ADD  CONSTRAINT [FK_GroupSubject_subject] FOREIGN KEY([subjectId])
REFERENCES [dbo].[Subject] ([id])
GO
ALTER TABLE [dbo].[GroupSubject] CHECK CONSTRAINT [FK_GroupSubject_subject]
GO
ALTER TABLE [dbo].[GroupSubject]  WITH CHECK ADD  CONSTRAINT [FK_GroupSubject_teacher] FOREIGN KEY([teacherId])
REFERENCES [dbo].[Teacher] ([id])
GO
ALTER TABLE [dbo].[GroupSubject] CHECK CONSTRAINT [FK_GroupSubject_teacher]
GO
ALTER TABLE [dbo].[Guardian]  WITH CHECK ADD  CONSTRAINT [FK_Guardian_user] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Guardian] CHECK CONSTRAINT [FK_Guardian_user]
GO
ALTER TABLE [dbo].[HabitCompliance]  WITH CHECK ADD  CONSTRAINT [FK_HabitCompliance_habit] FOREIGN KEY([habitId])
REFERENCES [dbo].[StudentHabit] ([id])
GO
ALTER TABLE [dbo].[HabitCompliance] CHECK CONSTRAINT [FK_HabitCompliance_habit]
GO
ALTER TABLE [dbo].[HabitCompliance]  WITH CHECK ADD  CONSTRAINT [FK_HabitCompliance_registeredBy] FOREIGN KEY([registeredById])
REFERENCES [dbo].[User] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[HabitCompliance] CHECK CONSTRAINT [FK_HabitCompliance_registeredBy]
GO
ALTER TABLE [dbo].[LearningContent]  WITH CHECK ADD  CONSTRAINT [FK_LearningContent_lesson] FOREIGN KEY([lessonId])
REFERENCES [dbo].[Lesson] ([id])
GO
ALTER TABLE [dbo].[LearningContent] CHECK CONSTRAINT [FK_LearningContent_lesson]
GO
ALTER TABLE [dbo].[LearningContent]  WITH CHECK ADD  CONSTRAINT [FK_LearningContent_subject] FOREIGN KEY([subjectId])
REFERENCES [dbo].[Subject] ([id])
GO
ALTER TABLE [dbo].[LearningContent] CHECK CONSTRAINT [FK_LearningContent_subject]
GO
ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_module] FOREIGN KEY([moduleId])
REFERENCES [dbo].[Module] ([id])
GO
ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [FK_Lesson_module]
GO
ALTER TABLE [dbo].[LessonStep]  WITH CHECK ADD  CONSTRAINT [FK_LessonStep_lesson] FOREIGN KEY([lessonId])
REFERENCES [dbo].[Lesson] ([id])
GO
ALTER TABLE [dbo].[LessonStep] CHECK CONSTRAINT [FK_LessonStep_lesson]
GO
ALTER TABLE [dbo].[LinkCode]  WITH CHECK ADD  CONSTRAINT [FK_LinkCode_issuedBy] FOREIGN KEY([issuedById])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[LinkCode] CHECK CONSTRAINT [FK_LinkCode_issuedBy]
GO
ALTER TABLE [dbo].[LinkCode]  WITH CHECK ADD  CONSTRAINT [FK_LinkCode_usedBy] FOREIGN KEY([usedById])
REFERENCES [dbo].[User] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[LinkCode] CHECK CONSTRAINT [FK_LinkCode_usedBy]
GO
ALTER TABLE [dbo].[Module]  WITH CHECK ADD  CONSTRAINT [FK_Module_subject] FOREIGN KEY([subjectId])
REFERENCES [dbo].[Subject] ([id])
GO
ALTER TABLE [dbo].[Module] CHECK CONSTRAINT [FK_Module_subject]
GO
ALTER TABLE [dbo].[PecsBoard]  WITH CHECK ADD  CONSTRAINT [FK_PecsBoard_student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[PecsBoard] CHECK CONSTRAINT [FK_PecsBoard_student]
GO
ALTER TABLE [dbo].[PecsCard]  WITH CHECK ADD  CONSTRAINT [FK_PecsCard_board] FOREIGN KEY([boardId])
REFERENCES [dbo].[PecsBoard] ([id])
GO
ALTER TABLE [dbo].[PecsCard] CHECK CONSTRAINT [FK_PecsCard_board]
GO
ALTER TABLE [dbo].[Routine]  WITH CHECK ADD  CONSTRAINT [FK_Routine_createdBy] FOREIGN KEY([createdByUserId])
REFERENCES [dbo].[User] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Routine] CHECK CONSTRAINT [FK_Routine_createdBy]
GO
ALTER TABLE [dbo].[Routine]  WITH CHECK ADD  CONSTRAINT [FK_Routine_student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[Routine] CHECK CONSTRAINT [FK_Routine_student]
GO
ALTER TABLE [dbo].[RoutineDetail]  WITH CHECK ADD  CONSTRAINT [FK_RoutineDetail_routine] FOREIGN KEY([routineId])
REFERENCES [dbo].[Routine] ([id])
GO
ALTER TABLE [dbo].[RoutineDetail] CHECK CONSTRAINT [FK_RoutineDetail_routine]
GO
ALTER TABLE [dbo].[RoutineLog]  WITH CHECK ADD  CONSTRAINT [FK_RoutineLog_detail] FOREIGN KEY([routineDetailId])
REFERENCES [dbo].[RoutineDetail] ([id])
GO
ALTER TABLE [dbo].[RoutineLog] CHECK CONSTRAINT [FK_RoutineLog_detail]
GO
ALTER TABLE [dbo].[RoutineLog]  WITH CHECK ADD  CONSTRAINT [FK_RoutineLog_registeredBy] FOREIGN KEY([registeredById])
REFERENCES [dbo].[User] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[RoutineLog] CHECK CONSTRAINT [FK_RoutineLog_registeredBy]
GO
ALTER TABLE [dbo].[RoutineLog]  WITH CHECK ADD  CONSTRAINT [FK_RoutineLog_student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[RoutineLog] CHECK CONSTRAINT [FK_RoutineLog_student]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_group] FOREIGN KEY([groupId])
REFERENCES [dbo].[ClassGroup] ([id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_group]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_user] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_user]
GO
ALTER TABLE [dbo].[StudentHabit]  WITH CHECK ADD  CONSTRAINT [FK_StudentHabit_student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[StudentHabit] CHECK CONSTRAINT [FK_StudentHabit_student]
GO
ALTER TABLE [dbo].[StudentHabit]  WITH CHECK ADD  CONSTRAINT [FK_StudentHabit_subject] FOREIGN KEY([subjectId])
REFERENCES [dbo].[Subject] ([id])
GO
ALTER TABLE [dbo].[StudentHabit] CHECK CONSTRAINT [FK_StudentHabit_subject]
GO
ALTER TABLE [dbo].[StudentInterest]  WITH CHECK ADD  CONSTRAINT [FK_StudentInterest_student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[StudentInterest] CHECK CONSTRAINT [FK_StudentInterest_student]
GO
ALTER TABLE [dbo].[StudentProgress]  WITH CHECK ADD  CONSTRAINT [FK_StudentProgress_student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[StudentProgress] CHECK CONSTRAINT [FK_StudentProgress_student]
GO
ALTER TABLE [dbo].[StudyHistory]  WITH CHECK ADD  CONSTRAINT [FK_StudyHistory_lesson] FOREIGN KEY([lessonId])
REFERENCES [dbo].[Lesson] ([id])
GO
ALTER TABLE [dbo].[StudyHistory] CHECK CONSTRAINT [FK_StudyHistory_lesson]
GO
ALTER TABLE [dbo].[StudyHistory]  WITH CHECK ADD  CONSTRAINT [FK_StudyHistory_student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[StudyHistory] CHECK CONSTRAINT [FK_StudyHistory_student]
GO
ALTER TABLE [dbo].[StudyHistory]  WITH CHECK ADD  CONSTRAINT [FK_StudyHistory_subject] FOREIGN KEY([subjectId])
REFERENCES [dbo].[Subject] ([id])
GO
ALTER TABLE [dbo].[StudyHistory] CHECK CONSTRAINT [FK_StudyHistory_subject]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_user] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_user]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_role] FOREIGN KEY([roleId])
REFERENCES [dbo].[Role] ([id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_role]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_user] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_user]
GO
ALTER TABLE [dbo].[EntityStudentRelation]  WITH CHECK ADD  CONSTRAINT [CK_EntityStudentRelation_entityType] CHECK  (([entityType]='TEACHER' OR [entityType]='GUARDIAN'))
GO
ALTER TABLE [dbo].[EntityStudentRelation] CHECK CONSTRAINT [CK_EntityStudentRelation_entityType]
GO
ALTER TABLE [dbo].[EntityStudentRelation]  WITH CHECK ADD  CONSTRAINT [CK_EntityStudentRelation_relationType] CHECK  (([relationType]='PRIMARY_GUARDIAN' OR [relationType]='GUARDIAN' OR [relationType]='REFERRING_TEACHER'))
GO
ALTER TABLE [dbo].[EntityStudentRelation] CHECK CONSTRAINT [CK_EntityStudentRelation_relationType]
GO
ALTER TABLE [dbo].[LearningContent]  WITH CHECK ADD  CONSTRAINT [CK_LearningContent_type] CHECK  (([type]='DICTIONARY' OR [type]='ROUTINE' OR [type]='EXCEPTION'))
GO
ALTER TABLE [dbo].[LearningContent] CHECK CONSTRAINT [CK_LearningContent_type]
GO
ALTER TABLE [dbo].[LinkCode]  WITH CHECK ADD  CONSTRAINT [CK_LinkCode_purpose] CHECK  (([purpose]='ENROLLMENT' OR [purpose]='TEACHER_CONTRACT'))
GO
ALTER TABLE [dbo].[LinkCode] CHECK CONSTRAINT [CK_LinkCode_purpose]
GO
ALTER TABLE [dbo].[LinkCode]  WITH CHECK ADD  CONSTRAINT [CK_LinkCode_status] CHECK  (([status]='PENDING' OR [status]='USED' OR [status]='EXPIRED' OR [status]='REVOKED'))
GO
ALTER TABLE [dbo].[LinkCode] CHECK CONSTRAINT [CK_LinkCode_status]
GO
ALTER TABLE [dbo].[LinkCode]  WITH CHECK ADD  CONSTRAINT [CK_LinkCode_targetEntityType] CHECK  (([targetEntityType]='STUDENT' OR [targetEntityType]='GUARDIAN' OR [targetEntityType]='TEACHER' OR [targetEntityType] IS NULL))
GO
ALTER TABLE [dbo].[LinkCode] CHECK CONSTRAINT [CK_LinkCode_targetEntityType]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [CK_Teacher_entityStatus] CHECK  (([entityStatus]='INACTIVE' OR [entityStatus]='ON_LEAVE' OR [entityStatus]='ACTIVE'))
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [CK_Teacher_entityStatus]
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateClassGroup] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_CreateClassGroup]
    @Name VARCHAR(120),
    @GradeLevel VARCHAR(50) = NULL,
    @Description VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO dbo.ClassGroup (name, gradeLevel, description, isActive, createdAt)
        OUTPUT INSERTED.id AS Id, INSERTED.name AS Name, INSERTED.gradeLevel AS GradeLevel,
               INSERTED.description AS Description, INSERTED.isActive AS IsActive, INSERTED.createdAt AS CreatedAt,
               CAST(0 AS INT) AS StudentCount
        VALUES (@Name, @GradeLevel, @Description, 1, GETDATE());

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateGroupSubject] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_CreateGroupSubject]
    @GroupId INT,
    @SubjectId INT,
    @TeacherId INT,
    @AssignmentDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.ClassGroup WHERE id = @GroupId)
            RETURN 50160;

        IF NOT EXISTS (SELECT 1 FROM dbo.Subject WHERE id = @SubjectId)
            RETURN 50150;

        IF NOT EXISTS (SELECT 1 FROM dbo.Teacher WHERE id = @TeacherId)
            RETURN 5090;

        DECLARE @EffectiveDate DATE = ISNULL(@AssignmentDate, CAST(GETDATE() AS DATE));

        IF EXISTS (
            SELECT 1 FROM dbo.GroupSubject
            WHERE groupId = @GroupId AND subjectId = @SubjectId AND teacherId = @TeacherId
              AND assignmentDate = @EffectiveDate
        )
            RETURN 50181;

        INSERT INTO dbo.GroupSubject (groupId, subjectId, teacherId, isActive, assignmentDate, createdAt)
        VALUES (@GroupId, @SubjectId, @TeacherId, 1, @EffectiveDate, GETDATE());

        DECLARE @NewId INT = SCOPE_IDENTITY();

        SELECT
            gs.id AS Id, gs.groupId AS GroupId, gs.subjectId AS SubjectId, gs.teacherId AS TeacherId,
            gs.isActive AS IsActive, gs.assignmentDate AS AssignmentDate, gs.endDate AS EndDate,
            s.name AS SubjectName, t.firstName AS TeacherFirstName, t.lastName AS TeacherLastName
        FROM dbo.GroupSubject gs
        INNER JOIN dbo.Subject s ON s.id = gs.subjectId
        INNER JOIN dbo.Teacher t ON t.id = gs.teacherId
        WHERE gs.id = @NewId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateGuardianLinkCode] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_CreateGuardianLinkCode]
    @GuardianId INT,
    @IssuedById INT = NULL,
    @ExpiresAt DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Guardian WHERE id = @GuardianId)
            RETURN 5095;

        IF EXISTS (SELECT 1 FROM dbo.Guardian WHERE id = @GuardianId AND userId IS NOT NULL)
            RETURN 5096;

        IF EXISTS (
            SELECT 1 FROM dbo.LinkCode
            WHERE targetEntityType = 'GUARDIAN' AND targetEntityId = @GuardianId AND status = 'PENDING'
        )
            RETURN 5082;

        DECLARE @NewCode VARCHAR(50);
        DECLARE @Attempts INT = 0;

        WHILE 1 = 1
        BEGIN
            SET @NewCode = UPPER(LEFT(REPLACE(CONVERT(VARCHAR(36), NEWID()), '-', ''), 10));
            IF NOT EXISTS (SELECT 1 FROM dbo.LinkCode WHERE code = @NewCode)
                BREAK;

            SET @Attempts += 1;
            IF @Attempts > 5
                RETURN 5099;
        END

        INSERT INTO dbo.LinkCode (code, purpose, status, issuedById, expiresAt, targetEntityType, targetEntityId, createdAt)
        OUTPUT
            INSERTED.id AS Id, INSERTED.code AS Code, INSERTED.purpose AS Purpose, INSERTED.status AS Status,
            INSERTED.issuedById AS IssuedById, INSERTED.expiresAt AS ExpiresAt,
            INSERTED.usedById AS UsedById, INSERTED.usedAt AS UsedAt,
            INSERTED.createdAt AS CreatedAt, INSERTED.updatedAt AS UpdateAt,
            INSERTED.targetEntityType AS TargetEntityType, INSERTED.targetEntityId AS TargetEntityId
        VALUES (@NewCode, 'ENROLLMENT', 'PENDING', @IssuedById, @ExpiresAt, 'GUARDIAN', @GuardianId, GETDATE());

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateStudent] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_CreateStudent]
    @GroupId INT,
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100) = NULL,
    @UniqueNumber VARCHAR(30) = NULL,
    @BirthDate DATE = NULL,
    @Gender VARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.ClassGroup WHERE id = @GroupId)
            RETURN 50171;

        IF (SELECT COUNT(*) FROM dbo.Student WHERE groupId = @GroupId AND isActive = 1) >= 10
            RETURN 50172;

        IF @UniqueNumber IS NOT NULL AND EXISTS (SELECT 1 FROM dbo.Student WHERE uniqueNumber = @UniqueNumber)
            RETURN 50173;

        INSERT INTO dbo.Student (groupId, firstName, lastName, uniqueNumber, birthDate, gender, isActive, createdAt)
        OUTPUT
            INSERTED.id AS Id, INSERTED.groupId AS GroupId, INSERTED.userId AS UserId,
            INSERTED.firstName AS FirstName, INSERTED.lastName AS LastName, INSERTED.uniqueNumber AS UniqueNumber,
            INSERTED.birthDate AS BirthDate, INSERTED.gender AS Gender, INSERTED.languageLevel AS LanguageLevel,
            INSERTED.clinicalInfo AS ClinicalInfo, INSERTED.observations AS Observations,
            INSERTED.isActive AS IsActive, INSERTED.createdAt AS CreatedAt, INSERTED.updatedAt AS UpdatedAt
        VALUES (@GroupId, @FirstName, @LastName, @UniqueNumber, @BirthDate, @Gender, 1, GETDATE());

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateStudentRelation] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_CreateStudentRelation]
    @EntityId INT,
    @EntityType VARCHAR(20),
    @StudentId INT,
    @RelationType VARCHAR(20),
    @AssignedAt DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Student WHERE id = @StudentId)
            RETURN 50170;

        IF @EntityType = 'TEACHER' AND NOT EXISTS (SELECT 1 FROM dbo.Teacher WHERE id = @EntityId)
            RETURN 50191;

        IF @EntityType = 'GUARDIAN' AND NOT EXISTS (SELECT 1 FROM dbo.Guardian WHERE id = @EntityId)
            RETURN 50191;

        IF @EntityType = 'TEACHER' AND @RelationType <> 'REFERRING_TEACHER'
            RETURN 50193;

        IF @EntityType = 'GUARDIAN' AND @RelationType NOT IN ('GUARDIAN', 'PRIMARY_GUARDIAN')
            RETURN 50193;

        IF EXISTS (
            SELECT 1 FROM dbo.EntityStudentRelation
            WHERE entityId = @EntityId AND entityType = @EntityType
              AND studentId = @StudentId AND relationType = @RelationType AND isActive = 1
        )
            RETURN 50194;

        IF @RelationType = 'PRIMARY_GUARDIAN' AND EXISTS (
            SELECT 1 FROM dbo.EntityStudentRelation
            WHERE studentId = @StudentId AND relationType = 'PRIMARY_GUARDIAN' AND isActive = 1
        )
            RETURN 50195;

        INSERT INTO dbo.EntityStudentRelation (entityId, entityType, studentId, relationType, isActive, assignedAt, createdAt)
        OUTPUT
            INSERTED.id AS Id, INSERTED.entityId AS EntityId, INSERTED.entityType AS EntityType,
            INSERTED.studentId AS StudentId, INSERTED.relationType AS RelationType,
            INSERTED.isActive AS IsActive, INSERTED.assignedAt AS AssignedAt, INSERTED.endDate AS EndDate
        VALUES (@EntityId, @EntityType, @StudentId, @RelationType, 1, ISNULL(@AssignedAt, CAST(GETDATE() AS DATE)), GETDATE());

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_CreateTeacherLinkCode] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_CreateTeacherLinkCode]
    @TeacherId INT,
    @IssuedById INT = NULL,
    @ExpiresAt DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Teacher WHERE id = @TeacherId)
            RETURN 5090;

        IF EXISTS (SELECT 1 FROM dbo.Teacher WHERE id = @TeacherId AND userId IS NOT NULL)
            RETURN 5091;

        IF EXISTS (
            SELECT 1 FROM dbo.LinkCode
            WHERE targetEntityType = 'TEACHER' AND targetEntityId = @TeacherId AND status = 'PENDING'
        )
            RETURN 5082;

        DECLARE @NewCode VARCHAR(50);
        DECLARE @Attempts INT = 0;

        WHILE 1 = 1
        BEGIN
            SET @NewCode = UPPER(LEFT(REPLACE(CONVERT(VARCHAR(36), NEWID()), '-', ''), 10));
            IF NOT EXISTS (SELECT 1 FROM dbo.LinkCode WHERE code = @NewCode)
                BREAK;

            SET @Attempts += 1;
            IF @Attempts > 5
                RETURN 5099;
        END

        INSERT INTO dbo.LinkCode (code, purpose, status, issuedById, expiresAt, targetEntityType, targetEntityId, createdAt)
        OUTPUT
            INSERTED.id AS Id, INSERTED.code AS Code, INSERTED.purpose AS Purpose, INSERTED.status AS Status,
            INSERTED.issuedById AS IssuedById, INSERTED.expiresAt AS ExpiresAt,
            INSERTED.usedById AS UsedById, INSERTED.usedAt AS UsedAt,
            INSERTED.createdAt AS CreatedAt, INSERTED.updatedAt AS UpdateAt,
            INSERTED.targetEntityType AS TargetEntityType, INSERTED.targetEntityId AS TargetEntityId
        VALUES (@NewCode, 'TEACHER_CONTRACT', 'PENDING', @IssuedById, @ExpiresAt, 'TEACHER', @TeacherId, GETDATE());

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_DeactivateGuardian] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_DeactivateGuardian]
    @GuardianId INT,
    @Reason VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Guardian WHERE id = @GuardianId)
            RETURN 5095;

        IF EXISTS (SELECT 1 FROM dbo.Guardian WHERE id = @GuardianId AND entityStatus = 'INACTIVE')
            RETURN 5098;

        DECLARE @UserId INT;
        SELECT @UserId = userId FROM dbo.Guardian WHERE id = @GuardianId;

        UPDATE dbo.Guardian
        SET entityStatus = 'INACTIVE',
            dismissalDate = CAST(GETDATE() AS DATE),
            dismissalReason = @Reason,
            updatedAt = GETDATE()
        WHERE id = @GuardianId;

        IF @UserId IS NOT NULL
        BEGIN
            UPDATE dbo.[User]
            SET isActive = 0, updatedAt = GETDATE()
            WHERE id = @UserId;
        END

        SELECT
            id AS Id, userId AS UserId, firstName AS FirstName, lastName AS LastName,
            entityStatus AS EntityStatus, dismissalDate AS DismissalDate,
            dismissalReason AS DismissalReason, updatedAt AS UpdatedAt
        FROM dbo.Guardian
        WHERE id = @GuardianId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_DeactivateTeacher] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_DeactivateTeacher]
    @TeacherId INT,
    @Reason VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Teacher WHERE id = @TeacherId)
            RETURN 5090;

        IF EXISTS (SELECT 1 FROM dbo.Teacher WHERE id = @TeacherId AND entityStatus = 'INACTIVE')
            RETURN 5093;

        DECLARE @UserId INT;
        SELECT @UserId = userId FROM dbo.Teacher WHERE id = @TeacherId;

        UPDATE dbo.Teacher
        SET entityStatus = 'INACTIVE',
            dismissalDate = CAST(GETDATE() AS DATE),
            dismissalReason = @Reason,
            updatedAt = GETDATE()
        WHERE id = @TeacherId;

        IF @UserId IS NOT NULL
        BEGIN
            UPDATE dbo.[User]
            SET isActive = 0, updatedAt = GETDATE()
            WHERE id = @UserId;
        END

        SELECT
            id AS Id, userId AS UserId, firstName AS FirstName, lastName AS LastName,
            nationalId AS NationalId, personalEmail AS PersonalEmail, phone AS Phone,
            address AS Address, city AS City, photo AS Photo, specialty AS Specialty,
            degree AS Degree, entityStatus AS EntityStatus,
            dismissalDate AS DismissalDate, dismissalReason AS DismissalReason,
            createdAt AS CreatedAt, updatedAt AS UpdateAt
        FROM dbo.Teacher
        WHERE id = @TeacherId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_EndStudentRelation] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_EndStudentRelation]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.EntityStudentRelation WHERE id = @Id)
            RETURN 50190;

        UPDATE dbo.EntityStudentRelation
        SET isActive = 0, endDate = CAST(GETDATE() AS DATE)
        WHERE id = @Id;

        SELECT
            id AS Id, entityId AS EntityId, entityType AS EntityType, studentId AS StudentId,
            relationType AS RelationType, isActive AS IsActive, assignedAt AS AssignedAt, endDate AS EndDate
        FROM dbo.EntityStudentRelation
        WHERE id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetActiveStudentsForEntity] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_GetActiveStudentsForEntity]
    @EntityId INT,
    @EntityType VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT
            r.id AS RelationId, r.relationType AS RelationType, r.assignedAt AS AssignedAt,
            s.id AS Id, s.groupId AS GroupId, s.firstName AS FirstName, s.lastName AS LastName,
            s.uniqueNumber AS UniqueNumber, s.birthDate AS BirthDate, s.gender AS Gender,
            s.languageLevel AS LanguageLevel, s.isActive AS IsActive
        FROM dbo.EntityStudentRelation r
        INNER JOIN dbo.Student s ON s.id = r.studentId
        WHERE r.entityId = @EntityId AND r.entityType = @EntityType AND r.isActive = 1
        ORDER BY s.firstName;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllClassGroups] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* =========================================================
   CLASSGROUP
   ========================================================= */

CREATE   PROCEDURE [dbo].[USP_GetAllClassGroups]
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @IsActive BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT
            g.id AS Id, g.name AS Name, g.gradeLevel AS GradeLevel, g.description AS Description,
            g.isActive AS IsActive, g.createdAt AS CreatedAt,
            (SELECT COUNT(*) FROM dbo.Student s WHERE s.groupId = g.id AND s.isActive = 1) AS StudentCount
        FROM dbo.ClassGroup g
        WHERE (@IsActive IS NULL OR g.isActive = @IsActive)
        ORDER BY g.id
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*) AS TotalRecords
        FROM dbo.ClassGroup g
        WHERE (@IsActive IS NULL OR g.isActive = @IsActive);

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllGuardians] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   GUARDIAN - Listado y baja (uso institucional)
   ========================================================= */

CREATE   PROCEDURE [dbo].[USP_GetAllGuardians]
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @Status VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT
            g.id AS Id,
            g.userId AS UserId,
            g.firstName AS FirstName,
            g.lastName AS LastName,
            g.nationalId AS NationalId,
            g.personalEmail AS PersonalEmail,
            g.phone AS Phone,
            g.relationship AS Relationship,
            g.entityStatus AS EntityStatus,
            u.email AS AccountEmail,
            u.isActive AS AccountIsActive,
            g.createdAt AS CreatedAt,
            g.updatedAt AS UpdatedAt
        FROM dbo.Guardian g
        LEFT JOIN dbo.[User] u ON u.id = g.userId
        WHERE (@Status IS NULL OR g.entityStatus = @Status)
        ORDER BY g.id
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*) AS TotalRecords
        FROM dbo.Guardian g
        WHERE (@Status IS NULL OR g.entityStatus = @Status);

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllLesson] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_GetAllLesson
Descripción: Obtiene todas las lecciones (paginado).
*****************************************************/
Create PROCEDURE [dbo].[USP_GetAllLesson]
    @PageNumber INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    -- Códigos personalizados
    -- 0     : Éxito
    -- 50180 : No hay registros disponibles

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Lesson)
        BEGIN
            RETURN 50180;
        END

        SELECT
            l.id,
            l.moduleId,
            l.title,
            l.description,
            l.type,
            l.durationMinutes,
            l.createdAt
        FROM Lesson AS l
        ORDER BY l.id
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*) AS TotalRecords FROM Lesson;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

/****** Object:  StoredProcedure [dbo].[USP_GetLessonById]    Script Date: 5/9/2026 19:15:05 ******/
SET ANSI_NULLS ON
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllLessonStep] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_GetAllLessonStep
Descripción: Obtiene todos los pasos de lección (paginado).
*****************************************************/
Create PROCEDURE [dbo].[USP_GetAllLessonStep]
    @PageNumber INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    -- Códigos personalizados
    -- 0     : Éxito
    -- 50190 : No hay registros disponibles

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM LessonStep)
        BEGIN
            RETURN 50190;
        END

        SELECT
            ls.id,
            ls.lessonId,
            ls.stepNumber,
            ls.title,
            ls.description,
            ls.contentType,
            ls.contentUrl,
            ls.isActive,
            ls.createdAt
        FROM LessonStep AS ls
        ORDER BY ls.lessonId, ls.stepNumber
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*) AS TotalRecords FROM LessonStep;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END


/****** Object:  StoredProcedure [dbo].[USP_GetLessonStepById]    Script Date: 5/9/2026 19:18:37 ******/
SET ANSI_NULLS ON
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllModule] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_GetAllModule
Descripción: Obtiene todos los módulos (paginado).
*****************************************************/
Create PROCEDURE [dbo].[USP_GetAllModule]
    @PageNumber INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;
    -- Códigos personalizados
    -- 0     : Éxito
    -- 50170 : No hay registros disponibles
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Module)
        BEGIN
            RETURN 50170;
        END

        SELECT
            m.id,
            m.subjectId,
            m.name,
            m.description,
            m.iconUrl,
            m.createdAt
        FROM Module AS m
        ORDER BY m.id
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*) AS TotalRecords FROM Module;
        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END


/****** Object:  StoredProcedure [dbo].[USP_GetModuleById]    Script Date: 5/9/2026 19:13:34 ******/
SET ANSI_NULLS ON
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllRole] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   STORED PROCEDURES
   ========================================================= */


/* =========================================================
   USP_GetAllRole
   ========================================================= */

CREATE PROCEDURE [dbo].[USP_GetAllRole]
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        IF NOT EXISTS (SELECT 1 FROM dbo.[Role])
            RETURN 50050;

        SELECT
            r.id,
            r.name,
            r.description,
            r.isActive,
            r.createdAt,
            r.updatedAt
        FROM dbo.[Role] AS r
        ORDER BY r.id;

        RETURN 0;

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllSubject] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   USP_GetAllSubject
   ========================================================= */

CREATE PROCEDURE [dbo].[USP_GetAllSubject]
    @PageNumber INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        IF NOT EXISTS (SELECT 1 FROM dbo.Subject)
            RETURN 50137;

        SELECT
            s.id,
            s.name,
            s.description,
            s.color,
            s.icon,
            s.createdAt
        FROM dbo.Subject AS s
        ORDER BY s.id
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*) AS TotalRecords
        FROM dbo.Subject;

        RETURN 0;

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetAllTeachers] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_GetAllTeachers]
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @Status VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT
            t.id AS Id, t.userId AS UserId, t.firstName AS FirstName, t.lastName AS LastName,
            t.nationalId AS NationalId, t.personalEmail AS PersonalEmail, t.phone AS Phone,
            t.address AS Address, t.city AS City, t.photo AS Photo,
            t.specialty AS Specialty, t.degree AS Degree, t.entityStatus AS EntityStatus,
            t.dismissalDate AS DismissalDate, t.dismissalReason AS DismissalReason,
            t.createdAt AS CreatedAt, t.updatedAt AS UpdateAt,
            u.email AS AccountEmail, u.isActive AS AccountIsActive
        FROM dbo.Teacher t
        LEFT JOIN dbo.[User] u ON u.id = t.userId
        WHERE (@Status IS NULL OR t.entityStatus = @Status)
        ORDER BY t.id
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*) AS TotalRecords
        FROM dbo.Teacher t
        WHERE (@Status IS NULL OR t.entityStatus = @Status);

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetClassGroupById] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_GetClassGroupById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.ClassGroup WHERE id = @Id)
            RETURN 50160;

        SELECT
            g.id AS Id, g.name AS Name, g.gradeLevel AS GradeLevel, g.description AS Description,
            g.isActive AS IsActive, g.createdAt AS CreatedAt,
            (SELECT COUNT(*) FROM dbo.Student s WHERE s.groupId = g.id AND s.isActive = 1) AS StudentCount
        FROM dbo.ClassGroup g
        WHERE g.id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetGroupSubjectsByGroup] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* =========================================================
   GROUPSUBJECT
   ========================================================= */

CREATE   PROCEDURE [dbo].[USP_GetGroupSubjectsByGroup]
    @GroupId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.ClassGroup WHERE id = @GroupId)
            RETURN 50160;

        SELECT
            gs.id AS Id, gs.groupId AS GroupId, gs.subjectId AS SubjectId, gs.teacherId AS TeacherId,
            gs.isActive AS IsActive, gs.assignmentDate AS AssignmentDate, gs.endDate AS EndDate,
            s.name AS SubjectName, t.firstName AS TeacherFirstName, t.lastName AS TeacherLastName
        FROM dbo.GroupSubject gs
        INNER JOIN dbo.Subject s ON s.id = gs.subjectId
        INNER JOIN dbo.Teacher t ON t.id = gs.teacherId
        WHERE gs.groupId = @GroupId
        ORDER BY gs.id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetGroupSubjectsByTeacher] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_GetGroupSubjectsByTeacher]
    @TeacherId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Teacher WHERE id = @TeacherId)
            RETURN 5090;

        SELECT
            gs.id AS Id, gs.groupId AS GroupId, gs.subjectId AS SubjectId, gs.teacherId AS TeacherId,
            gs.isActive AS IsActive, gs.assignmentDate AS AssignmentDate, gs.endDate AS EndDate,
            s.name AS SubjectName, g.name AS GroupName
        FROM dbo.GroupSubject gs
        INNER JOIN dbo.Subject s ON s.id = gs.subjectId
        INNER JOIN dbo.ClassGroup g ON g.id = gs.groupId
        WHERE gs.teacherId = @TeacherId AND gs.isActive = 1
        ORDER BY gs.id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetGuardianByUserId] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   GUARDIAN - Perfil
   ========================================================= */

CREATE   PROCEDURE [dbo].[USP_GetGuardianByUserId]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Guardian WHERE userId = @UserId)
            RETURN 5095;

        SELECT
            id AS Id, userId AS UserId, firstName AS FirstName, lastName AS LastName,
            nationalId AS NationalId, personalEmail AS PersonalEmail, phone AS Phone,
            address AS Address, city AS City, photo AS Photo, relationship AS Relationship,
            entityStatus AS EntityStatus, createdAt AS CreatedAt, updatedAt AS UpdatedAt
        FROM dbo.Guardian
        WHERE userId = @UserId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetLessonById] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_GetLessonById
Descripción: Obtiene una lección por su Id.
*****************************************************/
Create PROCEDURE [dbo].[USP_GetLessonById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Códigos personalizados
    -- 0     : Éxito
    -- 50181 : No existe la lección

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Lesson WHERE id = @Id)
        BEGIN
            RETURN 50181;
        END

        SELECT
            l.id,
            l.moduleId,
            l.title,
            l.description,
            l.type,
            l.durationMinutes,
            l.createdAt
        FROM Lesson AS l
        WHERE l.id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END


/****** Object:  StoredProcedure [dbo].[USP_InsertNewLesson]    Script Date: 5/9/2026 19:15:36 ******/
SET ANSI_NULLS ON
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetLessonStepById] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_GetLessonStepById
Descripción: Obtiene un paso de lección por su Id.
*****************************************************/
Create PROCEDURE [dbo].[USP_GetLessonStepById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Códigos personalizados
    -- 0     : Éxito
    -- 50191 : No existe el paso de lección

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM LessonStep WHERE id = @Id)
        BEGIN
            RETURN 50191;
        END

        SELECT
            ls.id,
            ls.lessonId,
            ls.stepNumber,
            ls.title,
            ls.description,
            ls.contentType,
            ls.contentUrl,
            ls.isActive,
            ls.createdAt
        FROM LessonStep AS ls
        WHERE ls.id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

/****** Object:  StoredProcedure [dbo].[USP_InsertNewLessonStep]    Script Date: 5/9/2026 19:18:50 ******/
SET ANSI_NULLS ON
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetLinkCodeInfo] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[USP_GetLinkCodeInfo]
    @Code VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.LinkCode WHERE code = @Code)
            RETURN 5080;

        SELECT
            id AS Id, code AS Code, purpose AS Purpose, status AS Status,
            issuedById AS IssuedById, expiresAt AS ExpiresAt,
            usedById AS UsedById, usedAt AS UsedAt,
            createdAt AS CreatedAt, updatedAt AS UpdateAt,
            targetEntityType AS TargetEntityType, targetEntityId AS TargetEntityId
        FROM dbo.LinkCode
        WHERE code = @Code;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetModuleById] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_GetModuleById
*****************************************************/
Create PROCEDURE [dbo].[USP_GetModuleById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    -- 0     : Éxito
    -- 50171 : No existe registro con el Id proporcionado
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Module WHERE id = @Id)
        BEGIN
            RETURN 50171;
        END

        SELECT
            m.id,
            m.subjectId,
            m.name,
            m.description,
            m.iconUrl,
            m.createdAt
        FROM Module AS m
        WHERE m.id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END


/****** Object:  StoredProcedure [dbo].[USP_InsertNewModule]    Script Date: 5/9/2026 19:13:54 ******/
SET ANSI_NULLS ON
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetRelationsByStudent] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   ENTITYSTUDENTRELATION
   ========================================================= */

CREATE   PROCEDURE [dbo].[USP_GetRelationsByStudent]
    @StudentId INT,
    @OnlyActive BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Student WHERE id = @StudentId)
            RETURN 50170;

        SELECT
            r.id AS Id, r.entityId AS EntityId, r.entityType AS EntityType, r.studentId AS StudentId,
            r.relationType AS RelationType, r.isActive AS IsActive, r.assignedAt AS AssignedAt, r.endDate AS EndDate,
            CASE WHEN r.entityType = 'TEACHER' THEN t.firstName ELSE g.firstName END AS EntityFirstName,
            CASE WHEN r.entityType = 'TEACHER' THEN t.lastName ELSE g.lastName END AS EntityLastName
        FROM dbo.EntityStudentRelation r
        LEFT JOIN dbo.Teacher t ON t.id = r.entityId AND r.entityType = 'TEACHER'
        LEFT JOIN dbo.Guardian g ON g.id = r.entityId AND r.entityType = 'GUARDIAN'
        WHERE r.studentId = @StudentId AND (@OnlyActive = 0 OR r.isActive = 1)
        ORDER BY r.id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetRoleById] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   USP_GetRoleById
   ========================================================= */

CREATE PROCEDURE [dbo].[USP_GetRoleById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.[Role]
            WHERE id = @Id
        )
            RETURN 50050;

        SELECT
            r.id,
            r.name,
            r.description,
            r.isActive,
            r.createdAt,
            r.updatedAt
        FROM dbo.[Role] AS r
        WHERE r.id = @Id;

        RETURN 0;

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetStudentById] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_GetStudentById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Student WHERE id = @Id)
            RETURN 50170;

        SELECT
            id AS Id, groupId AS GroupId, userId AS UserId, firstName AS FirstName, lastName AS LastName,
            uniqueNumber AS UniqueNumber, birthDate AS BirthDate, gender AS Gender,
            languageLevel AS LanguageLevel, clinicalInfo AS ClinicalInfo, observations AS Observations,
            isActive AS IsActive, createdAt AS CreatedAt, updatedAt AS UpdatedAt
        FROM dbo.Student
        WHERE id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetStudentsByGroup] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   STUDENT
   ========================================================= */

CREATE   PROCEDURE [dbo].[USP_GetStudentsByGroup]
    @GroupId INT,
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @OnlyActive BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.ClassGroup WHERE id = @GroupId)
            RETURN 50160;

        SELECT
            id AS Id, groupId AS GroupId, userId AS UserId, firstName AS FirstName, lastName AS LastName,
            uniqueNumber AS UniqueNumber, birthDate AS BirthDate, gender AS Gender,
            languageLevel AS LanguageLevel, clinicalInfo AS ClinicalInfo, observations AS Observations,
            isActive AS IsActive, createdAt AS CreatedAt, updatedAt AS UpdatedAt
        FROM dbo.Student
        WHERE groupId = @GroupId AND (@OnlyActive = 0 OR isActive = 1)
        ORDER BY id
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*) AS TotalRecords
        FROM dbo.Student
        WHERE groupId = @GroupId AND (@OnlyActive = 0 OR isActive = 1);

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetSubjectById] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   USP_GetSubjectById
   ========================================================= */

CREATE PROCEDURE [dbo].[USP_GetSubjectById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.Subject
            WHERE id = @Id
        )
            RETURN 50150;

        SELECT
            s.id,
            s.name,
            s.description,
            s.color,
            s.icon,
            s.createdAt
        FROM dbo.Subject AS s
        WHERE s.id = @Id;

        RETURN 0;

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetSubjectByName] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   USP_GetSubjectByName
   ========================================================= */

CREATE PROCEDURE [dbo].[USP_GetSubjectByName]
    @Name VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.Subject
            WHERE name = @Name
        )
            RETURN 50151;

        SELECT
            s.id,
            s.name,
            s.description,
            s.color,
            s.icon,
            s.createdAt
        FROM dbo.Subject AS s
        WHERE s.name = @Name;

        RETURN 0;

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetTeacherByUserId] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[USP_GetTeacherByUserId]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Teacher WHERE userId = @UserId)
            RETURN 5090;

        SELECT
            id AS Id, userId AS UserId, firstName AS FirstName, lastName AS LastName,
            nationalId AS NationalId, personalEmail AS PersonalEmail, phone AS Phone,
            address AS Address, city AS City, photo AS Photo, specialty AS Specialty,
            degree AS Degree, entityStatus AS EntityStatus,
            dismissalDate AS DismissalDate, dismissalReason AS DismissalReason,
            createdAt AS CreatedAt, updatedAt AS UpdateAt
        FROM dbo.Teacher
        WHERE userId = @UserId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetUserByEmail] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_GetUserByEmail]
    @Email VARCHAR(120)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE email = @Email)
            RETURN 5060;

        SELECT
            u.id AS Id,
            u.email AS Email,
            u.passwordHash AS PasswordHash,
            u.isActive AS IsActive,
            (SELECT STRING_AGG(r.name, ',')
             FROM dbo.UserRole ur
             INNER JOIN dbo.[Role] r ON r.id = ur.roleId
             WHERE ur.userId = u.id) AS Roles
        FROM dbo.[User] u
        WHERE u.email = @Email;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_GetUserById] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[USP_GetUserById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE id = @Id)
            RETURN 5060;

        SELECT
            u.id AS Id,
            u.email AS Email,
            u.passwordHash AS PasswordHash,
            u.isActive AS IsActive,
            (SELECT STRING_AGG(r.name, ',')
             FROM dbo.UserRole ur
             INNER JOIN dbo.[Role] r ON r.id = ur.roleId
             WHERE ur.userId = u.id) AS Roles
        FROM dbo.[User] u
        WHERE u.id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_InsertNewLesson] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_InsertNewLesson
Descripción: Inserta una nueva lección.
*****************************************************/
Create PROCEDURE [dbo].[USP_InsertNewLesson]
    @ModuleId INT,
    @Title NVARCHAR(150),
    @Description NVARCHAR(MAX) = NULL,
    @Type NVARCHAR(50) = NULL,
    @DurationMinutes INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Códigos personalizados
    -- 0     : Éxito
    -- 50182 : No existe el módulo (ModuleId) proporcionado

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Module WHERE id = @ModuleId)
        BEGIN
            RETURN 50182;
        END

        DECLARE @NewId INT;

        INSERT INTO Lesson (moduleId, title, description, type, durationMinutes, createdAt)
        VALUES (@ModuleId, @Title, @Description, @Type, @DurationMinutes, GETDATE());

        SET @NewId = SCOPE_IDENTITY();

        SELECT
            l.id,
            l.moduleId,
            l.title,
            l.description,
            l.type,
            l.durationMinutes,
            l.createdAt
        FROM Lesson AS l
        WHERE l.id = @NewId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

/****** Object:  StoredProcedure [dbo].[USP_UpdateLesson]    Script Date: 5/9/2026 19:15:49 ******/
SET ANSI_NULLS ON
GO
/****** Objeto: StoredProcedure [dbo].[USP_InsertNewLessonStep] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_InsertNewLessonStep
Descripción: Inserta un nuevo paso de lección.
*****************************************************/
Create PROCEDURE [dbo].[USP_InsertNewLessonStep]
    @LessonId INT,
    @StepNumber INT = NULL,
    @Title NVARCHAR(150) = NULL,
    @Description NVARCHAR(MAX) = NULL,
    @ContentType NVARCHAR(50) = NULL,
    @ContentUrl NVARCHAR(500) = NULL,
    @IsActive BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Códigos personalizados
    -- 0     : Éxito
    -- 50192 : No existe la lección (LessonId) proporcionada

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Lesson WHERE id = @LessonId)
        BEGIN
            RETURN 50192;
        END

        DECLARE @NewId INT;

        INSERT INTO LessonStep (lessonId, stepNumber, title, description, contentType, contentUrl, isActive, createdAt)
        VALUES (@LessonId, @StepNumber, @Title, @Description, @ContentType, @ContentUrl, @IsActive, GETDATE());

        SET @NewId = SCOPE_IDENTITY();

        SELECT
            ls.id,
            ls.lessonId,
            ls.stepNumber,
            ls.title,
            ls.description,
            ls.contentType,
            ls.contentUrl,
            ls.isActive,
            ls.createdAt
        FROM LessonStep AS ls
        WHERE ls.id = @NewId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END


/****** Object:  StoredProcedure [dbo].[USP_UpdateLessonStep]    Script Date: 5/9/2026 19:19:04 ******/
SET ANSI_NULLS ON
GO
/****** Objeto: StoredProcedure [dbo].[USP_InsertNewModule] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_InsertNewModule
Descripción: Inserta un nuevo módulo asociado a una materia.
*****************************************************/
Create PROCEDURE [dbo].[USP_InsertNewModule]
    @SubjectId INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(MAX) = NULL,
    @IconUrl NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Códigos personalizados
    -- 0     : Éxito
    -- 50172 : No existe la materia (SubjectId) proporcionada
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Subject WHERE id = @SubjectId)
        BEGIN
            RETURN 50172;
        END

        DECLARE @NewId INT;

        INSERT INTO Module (subjectId, name, description, iconUrl, createdAt)
        VALUES (@SubjectId, @Name, @Description, @IconUrl, GETDATE());

        SET @NewId = SCOPE_IDENTITY();

        SELECT
            m.id,
            m.subjectId,
            m.name,
            m.description,
            m.iconUrl,
            m.createdAt
        FROM Module AS m
        WHERE m.id = @NewId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END



/****** Object:  StoredProcedure [dbo].[USP_UpdateModule]    Script Date: 5/9/2026 19:14:11 ******/
SET ANSI_NULLS ON
GO
/****** Objeto: StoredProcedure [dbo].[USP_InsertNewSubject] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   USP_InsertNewSubject
   ========================================================= */

CREATE PROCEDURE [dbo].[USP_InsertNewSubject]
    @Name VARCHAR(100),
    @Description VARCHAR(MAX) = NULL,
    @Color VARCHAR(20) = NULL,
    @Icon VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        IF EXISTS
        (
            SELECT 1
            FROM dbo.Subject
            WHERE name = @Name
        )
            RETURN 50130;

        INSERT INTO dbo.Subject
        (
            name,
            description,
            color,
            icon
        )
        OUTPUT INSERTED.*
        VALUES
        (
            @Name,
            @Description,
            @Color,
            @Icon
        );

        RETURN 0;

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_InsertRole] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   USP_InsertRole
   ========================================================= */

CREATE PROCEDURE [dbo].[USP_InsertRole]
    @Name VARCHAR(30),
    @Description VARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        IF EXISTS
        (
            SELECT 1
            FROM dbo.[Role]
            WHERE name = @Name
        )
            RETURN 50020;

        INSERT INTO dbo.[Role]
        (
            name,
            description,
            isActive,
            createdAt,
            updatedAt
        )
        OUTPUT INSERTED.*
        VALUES
        (
            @Name,
            @Description,
            1,
            GETDATE(),
            NULL
        );

        RETURN 0;

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_PatchTeacherProfile] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[USP_PatchTeacherProfile]
    @UserId INT,
    @FirstName VARCHAR(100) = NULL,
    @LastName VARCHAR(100) = NULL,
    @NationalId VARCHAR(16) = NULL,
    @PersonalEmail VARCHAR(120) = NULL,
    @Phone VARCHAR(20) = NULL,
    @Address VARCHAR(200) = NULL,
    @City VARCHAR(100) = NULL,
    @Photo VARCHAR(255) = NULL,
    @Specialty VARCHAR(100) = NULL,
    @Degree VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Teacher WHERE userId = @UserId)
            RETURN 5090;

        IF @NationalId IS NOT NULL AND EXISTS (
            SELECT 1 FROM dbo.Teacher WHERE nationalId = @NationalId AND userId <> @UserId
        )
            RETURN 5092;

        UPDATE dbo.Teacher
        SET firstName     = COALESCE(@FirstName, firstName),
            lastName      = COALESCE(@LastName, lastName),
            nationalId    = COALESCE(@NationalId, nationalId),
            personalEmail = COALESCE(@PersonalEmail, personalEmail),
            phone         = COALESCE(@Phone, phone),
            address       = COALESCE(@Address, address),
            city          = COALESCE(@City, city),
            photo         = COALESCE(@Photo, photo),
            specialty     = COALESCE(@Specialty, specialty),
            degree        = COALESCE(@Degree, degree),
            updatedAt     = GETDATE()
        WHERE userId = @UserId;

        SELECT
            id AS Id, userId AS UserId, firstName AS FirstName, lastName AS LastName,
            nationalId AS NationalId, personalEmail AS PersonalEmail, phone AS Phone,
            address AS Address, city AS City, photo AS Photo, specialty AS Specialty,
            degree AS Degree, entityStatus AS EntityStatus,
            dismissalDate AS DismissalDate, dismissalReason AS DismissalReason,
            createdAt AS CreatedAt, updatedAt AS UpdateAt
        FROM dbo.Teacher
        WHERE userId = @UserId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_RegisterUserWithLinkCode] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_RegisterUserWithLinkCode]
    @Code VARCHAR(50),
    @Email VARCHAR(120),
    @PasswordHash VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DECLARE @Purpose VARCHAR(20), @TargetType VARCHAR(20), @TargetId INT,
                @NewUserId INT, @RoleId INT, @RoleName VARCHAR(50);

        IF NOT EXISTS (SELECT 1 FROM dbo.LinkCode WHERE code = @Code)
            RETURN 5080;

        SELECT @Purpose = purpose, @TargetType = targetEntityType, @TargetId = targetEntityId
        FROM dbo.LinkCode WHERE code = @Code;

        IF EXISTS (
            SELECT 1 FROM dbo.LinkCode
            WHERE code = @Code
              AND (status <> 'PENDING' OR (expiresAt IS NOT NULL AND expiresAt < GETDATE()))
        )
            RETURN 5081;

        IF EXISTS (SELECT 1 FROM dbo.[User] WHERE email = @Email)
            RETURN 5061;

        SET @RoleName = CASE @Purpose
                            WHEN 'TEACHER_CONTRACT' THEN 'DOCENTE'
                            WHEN 'ENROLLMENT' THEN 'TUTOR'
                            ELSE NULL
                        END;

        SELECT @RoleId = id FROM dbo.[Role] WHERE name = @RoleName;
        IF @RoleId IS NULL
            RETURN 5050;

        BEGIN TRANSACTION;

            INSERT INTO dbo.[User] (email, passwordHash, isActive, createdAt)
            VALUES (@Email, @PasswordHash, 1, GETDATE());

            SET @NewUserId = SCOPE_IDENTITY();

            IF @Purpose = 'TEACHER_CONTRACT' AND @TargetType = 'TEACHER' AND @TargetId IS NOT NULL
                UPDATE dbo.Teacher SET userId = @NewUserId, updatedAt = GETDATE() WHERE id = @TargetId;
            ELSE IF @Purpose = 'ENROLLMENT' AND @TargetType = 'GUARDIAN' AND @TargetId IS NOT NULL
                UPDATE dbo.Guardian SET userId = @NewUserId, updatedAt = GETDATE() WHERE id = @TargetId;

            INSERT INTO dbo.UserRole (userId, roleId, assignedAt)
            VALUES (@NewUserId, @RoleId, GETDATE());

            UPDATE dbo.LinkCode
            SET status = 'USED', usedById = @NewUserId, usedAt = GETDATE(), updatedAt = GETDATE()
            WHERE code = @Code;

        COMMIT TRANSACTION;

        SELECT
            u.id AS Id,
            u.email AS Email,
            u.isActive AS IsActive,
            (SELECT STRING_AGG(r.name, ',')
             FROM dbo.UserRole ur
             INNER JOIN dbo.[Role] r ON r.id = ur.roleId
             WHERE ur.userId = u.id) AS Roles
        FROM dbo.[User] u
        WHERE u.id = @NewUserId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_RevokeLinkCode] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_RevokeLinkCode]
    @Code VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.LinkCode WHERE code = @Code)
            RETURN 5080;

        IF EXISTS (SELECT 1 FROM dbo.LinkCode WHERE code = @Code AND status <> 'PENDING')
            RETURN 5081;

        UPDATE dbo.LinkCode
        SET status = 'REVOKED', updatedAt = GETDATE()
        WHERE code = @Code;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_SetClassGroupActive] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_SetClassGroupActive]
    @Id INT,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.ClassGroup WHERE id = @Id)
            RETURN 50160;

        UPDATE dbo.ClassGroup SET isActive = @IsActive WHERE id = @Id;

        SELECT
            g.id AS Id, g.name AS Name, g.gradeLevel AS GradeLevel, g.description AS Description,
            g.isActive AS IsActive, g.createdAt AS CreatedAt,
            (SELECT COUNT(*) FROM dbo.Student s WHERE s.groupId = g.id AND s.isActive = 1) AS StudentCount
        FROM dbo.ClassGroup g
        WHERE g.id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_SetGroupSubjectActive] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_SetGroupSubjectActive]
    @Id INT,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.GroupSubject WHERE id = @Id)
            RETURN 50180;

        UPDATE dbo.GroupSubject
        SET isActive = @IsActive,
            endDate = CASE WHEN @IsActive = 0 THEN CAST(GETDATE() AS DATE) ELSE NULL END
        WHERE id = @Id;

        SELECT
            gs.id AS Id, gs.groupId AS GroupId, gs.subjectId AS SubjectId, gs.teacherId AS TeacherId,
            gs.isActive AS IsActive, gs.assignmentDate AS AssignmentDate, gs.endDate AS EndDate,
            s.name AS SubjectName, t.firstName AS TeacherFirstName, t.lastName AS TeacherLastName
        FROM dbo.GroupSubject gs
        INNER JOIN dbo.Subject s ON s.id = gs.subjectId
        INNER JOIN dbo.Teacher t ON t.id = gs.teacherId
        WHERE gs.id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_SetStudentActive] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_SetStudentActive]
    @Id INT,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Student WHERE id = @Id)
            RETURN 50170;

        IF @IsActive = 1
        BEGIN
            DECLARE @GroupId INT;
            SELECT @GroupId = groupId FROM dbo.Student WHERE id = @Id;

            IF (SELECT COUNT(*) FROM dbo.Student WHERE groupId = @GroupId AND isActive = 1) >= 10
                RETURN 50172;
        END

        UPDATE dbo.Student SET isActive = @IsActive, updatedAt = GETDATE() WHERE id = @Id;

        SELECT
            id AS Id, groupId AS GroupId, userId AS UserId, firstName AS FirstName, lastName AS LastName,
            uniqueNumber AS UniqueNumber, birthDate AS BirthDate, gender AS Gender,
            languageLevel AS LanguageLevel, clinicalInfo AS ClinicalInfo, observations AS Observations,
            isActive AS IsActive, createdAt AS CreatedAt, updatedAt AS UpdatedAt
        FROM dbo.Student
        WHERE id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateClassGroup] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_UpdateClassGroup]
    @Id INT,
    @Name VARCHAR(120),
    @GradeLevel VARCHAR(50) = NULL,
    @Description VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.ClassGroup WHERE id = @Id)
            RETURN 50160;

        UPDATE dbo.ClassGroup
        SET name = @Name, gradeLevel = @GradeLevel, description = @Description
        WHERE id = @Id;

        SELECT
            g.id AS Id, g.name AS Name, g.gradeLevel AS GradeLevel, g.description AS Description,
            g.isActive AS IsActive, g.createdAt AS CreatedAt,
            (SELECT COUNT(*) FROM dbo.Student s WHERE s.groupId = g.id AND s.isActive = 1) AS StudentCount
        FROM dbo.ClassGroup g
        WHERE g.id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateGuardianProfile] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_UpdateGuardianProfile]
    @UserId INT,
    @FirstName VARCHAR(100) = NULL,
    @LastName VARCHAR(100) = NULL,
    @NationalId VARCHAR(16) = NULL,
    @PersonalEmail VARCHAR(120) = NULL,
    @Phone VARCHAR(20) = NULL,
    @Address VARCHAR(200) = NULL,
    @City VARCHAR(100) = NULL,
    @Photo VARCHAR(255) = NULL,
    @Relationship VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Guardian WHERE userId = @UserId)
            RETURN 5095;

        IF @NationalId IS NOT NULL AND EXISTS (
            SELECT 1 FROM dbo.Guardian WHERE nationalId = @NationalId AND userId <> @UserId
        )
            RETURN 5097;

        UPDATE dbo.Guardian
        SET firstName    = COALESCE(@FirstName, firstName),
            lastName      = COALESCE(@LastName, lastName),
            nationalId    = COALESCE(@NationalId, nationalId),
            personalEmail = COALESCE(@PersonalEmail, personalEmail),
            phone         = COALESCE(@Phone, phone),
            address       = COALESCE(@Address, address),
            city          = COALESCE(@City, city),
            photo         = COALESCE(@Photo, photo),
            relationship  = COALESCE(@Relationship, relationship),
            updatedAt     = GETDATE()
        WHERE userId = @UserId;

        SELECT
            id AS Id, userId AS UserId, firstName AS FirstName, lastName AS LastName,
            nationalId AS NationalId, personalEmail AS PersonalEmail, phone AS Phone,
            address AS Address, city AS City, photo AS Photo, relationship AS Relationship,
            entityStatus AS EntityStatus, createdAt AS CreatedAt, updatedAt AS UpdatedAt
        FROM dbo.Guardian
        WHERE userId = @UserId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateLesson] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_UpdateLesson
Descripción: Actualiza una lección existente.
*****************************************************/
Create PROCEDURE [dbo].[USP_UpdateLesson]
    @Id INT,
    @ModuleId INT,
    @Title NVARCHAR(150),
    @Description NVARCHAR(MAX) = NULL,
    @Type NVARCHAR(50) = NULL,
    @DurationMinutes INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Códigos personalizados
    -- 0     : Éxito
    -- 50181 : No existe la lección
    -- 50182 : No existe el módulo (ModuleId) proporcionado

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Lesson WHERE id = @Id)
        BEGIN
            RETURN 50181;
        END

        IF NOT EXISTS (SELECT 1 FROM Module WHERE id = @ModuleId)
        BEGIN
            RETURN 50182;
        END

        UPDATE Lesson
        SET moduleId = @ModuleId,
            title = @Title,
            description = @Description,
            type = @Type,
            durationMinutes = @DurationMinutes
        WHERE id = @Id;

        SELECT
            l.id,
            l.moduleId,
            l.title,
            l.description,
            l.type,
            l.durationMinutes,
            l.createdAt
        FROM Lesson AS l
        WHERE l.id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateLessonStep] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_UpdateLessonStep
Descripción: Actualiza un paso de lección existente.
*****************************************************/
Create PROCEDURE [dbo].[USP_UpdateLessonStep]
    @Id INT,
    @LessonId INT,
    @StepNumber INT = NULL,
    @Title NVARCHAR(150) = NULL,
    @Description NVARCHAR(MAX) = NULL,
    @ContentType NVARCHAR(50) = NULL,
    @ContentUrl NVARCHAR(500) = NULL,
    @IsActive BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Códigos personalizados
    -- 0     : Éxito
    -- 50191 : No existe el paso de lección
    -- 50192 : No existe la lección (LessonId) proporcionada

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM LessonStep WHERE id = @Id)
        BEGIN
            RETURN 50191;
        END

        IF NOT EXISTS (SELECT 1 FROM Lesson WHERE id = @LessonId)
        BEGIN
            RETURN 50192;
        END

        UPDATE LessonStep
        SET lessonId = @LessonId,
            stepNumber = @StepNumber,
            title = @Title,
            description = @Description,
            contentType = @ContentType,
            contentUrl = @ContentUrl,
            isActive = @IsActive
        WHERE id = @Id;

        SELECT
            ls.id,
            ls.lessonId,
            ls.stepNumber,
            ls.title,
            ls.description,
            ls.contentType,
            ls.contentUrl,
            ls.isActive,
            ls.createdAt
        FROM LessonStep AS ls
        WHERE ls.id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateModule] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************
Nombre: USP_UpdateModule
Descripción: Actualiza un módulo existente.
*****************************************************/
Create PROCEDURE [dbo].[USP_UpdateModule]
    @Id INT,
    @SubjectId INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(MAX) = NULL,
    @IconUrl NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Códigos personalizados
    -- 0     : Éxito
    -- 50171 : No existe registro con el Id proporcionado
    -- 50172 : No existe la materia (SubjectId) proporcionada
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Module WHERE id = @Id)
        BEGIN
            RETURN 50171;
        END

        IF NOT EXISTS (SELECT 1 FROM Subject WHERE id = @SubjectId)
        BEGIN
            RETURN 50172;
        END

        UPDATE Module
        SET subjectId = @SubjectId,
            name = @Name,
            description = @Description,
            iconUrl = @IconUrl
        WHERE id = @Id;

        SELECT
            m.id,
            m.subjectId,
            m.name,
            m.description,
            m.iconUrl,
            m.createdAt
        FROM Module AS m
        WHERE m.id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateRole] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   USP_UpdateRole
   ========================================================= */

CREATE PROCEDURE [dbo].[USP_UpdateRole]
    @Id INT,
    @Name VARCHAR(30),
    @Description VARCHAR(200) = NULL,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.[Role]
            WHERE id = @Id
        )
            RETURN 50050;

        UPDATE dbo.[Role]
        SET
            name = @Name,
            description = @Description,
            isActive = @IsActive,
            updatedAt = GETDATE()
        WHERE id = @Id;

        SELECT
            r.id,
            r.name,
            r.description,
            r.isActive,
            r.createdAt,
            r.updatedAt
        FROM dbo.[Role] AS r
        WHERE r.id = @Id;

        RETURN 0;

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateStudent] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_UpdateStudent]
    @Id INT,
    @GroupId INT,
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100) = NULL,
    @UniqueNumber VARCHAR(30) = NULL,
    @BirthDate DATE = NULL,
    @Gender VARCHAR(10) = NULL,
    @LanguageLevel VARCHAR(100) = NULL,
    @ClinicalInfo VARCHAR(MAX) = NULL,
    @Observations VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Student WHERE id = @Id)
            RETURN 50170;

        IF NOT EXISTS (SELECT 1 FROM dbo.ClassGroup WHERE id = @GroupId)
            RETURN 50171;

        DECLARE @CurrentGroupId INT;
        SELECT @CurrentGroupId = groupId FROM dbo.Student WHERE id = @Id;

        IF @CurrentGroupId <> @GroupId AND (SELECT COUNT(*) FROM dbo.Student WHERE groupId = @GroupId AND isActive = 1) >= 10
            RETURN 50172;

        IF @UniqueNumber IS NOT NULL AND EXISTS (SELECT 1 FROM dbo.Student WHERE uniqueNumber = @UniqueNumber AND id <> @Id)
            RETURN 50173;

        UPDATE dbo.Student
        SET groupId = @GroupId,
            firstName = @FirstName,
            lastName = @LastName,
            uniqueNumber = @UniqueNumber,
            birthDate = @BirthDate,
            gender = @Gender,
            languageLevel = @LanguageLevel,
            clinicalInfo = @ClinicalInfo,
            observations = @Observations,
            updatedAt = GETDATE()
        WHERE id = @Id;

        SELECT
            id AS Id, groupId AS GroupId, userId AS UserId, firstName AS FirstName, lastName AS LastName,
            uniqueNumber AS UniqueNumber, birthDate AS BirthDate, gender AS Gender,
            languageLevel AS LanguageLevel, clinicalInfo AS ClinicalInfo, observations AS Observations,
            isActive AS IsActive, createdAt AS CreatedAt, updatedAt AS UpdatedAt
        FROM dbo.Student
        WHERE id = @Id;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateSubject] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   USP_UpdateSubject
   ========================================================= */

CREATE PROCEDURE [dbo].[USP_UpdateSubject]
    @Id INT,
    @Name VARCHAR(100),
    @Description VARCHAR(MAX) = NULL,
    @Color VARCHAR(20) = NULL,
    @Icon VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.Subject
            WHERE id = @Id
        )
            RETURN 50150;

        UPDATE dbo.Subject
        SET
            name = @Name,
            description = @Description,
            color = @Color,
            icon = @Icon
        WHERE id = @Id;

        SELECT
            id,
            name,
            description,
            color,
            icon,
            createdAt
        FROM dbo.Subject
        WHERE id = @Id;

        RETURN 0;

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateTeacherProfile] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[USP_UpdateTeacherProfile]
    @UserId INT,
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @NationalId VARCHAR(16) = NULL,
    @PersonalEmail VARCHAR(120) = NULL,
    @Phone VARCHAR(20) = NULL,
    @Address VARCHAR(200) = NULL,
    @City VARCHAR(100) = NULL,
    @Photo VARCHAR(255) = NULL,
    @Specialty VARCHAR(100) = NULL,
    @Degree VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Teacher WHERE userId = @UserId)
            RETURN 5090;

        IF @NationalId IS NOT NULL AND EXISTS (
            SELECT 1 FROM dbo.Teacher WHERE nationalId = @NationalId AND userId <> @UserId
        )
            RETURN 5092;

        UPDATE dbo.Teacher
        SET firstName = @FirstName,
            lastName = @LastName,
            nationalId = @NationalId,
            personalEmail = @PersonalEmail,
            phone = @Phone,
            address = @Address,
            city = @City,
            photo = @Photo,
            specialty = @Specialty,
            degree = @Degree,
            updatedAt = GETDATE()
        WHERE userId = @UserId;

        SELECT
            id AS Id, userId AS UserId, firstName AS FirstName, lastName AS LastName,
            nationalId AS NationalId, personalEmail AS PersonalEmail, phone AS Phone,
            address AS Address, city AS City, photo AS Photo, specialty AS Specialty,
            degree AS Degree, entityStatus AS EntityStatus,
            dismissalDate AS DismissalDate, dismissalReason AS DismissalReason,
            createdAt AS CreatedAt, updatedAt AS UpdateAt
        FROM dbo.Teacher
        WHERE userId = @UserId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Objeto: StoredProcedure [dbo].[USP_UpdateUserPassword] Fecha de script: 05/09/2026 07:50:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =========================================================
   USP_UpdateUserPassword (no existia en esta version)
   ========================================================= */

CREATE   PROCEDURE [dbo].[USP_UpdateUserPassword]
    @UserId INT,
    @NewPasswordHash VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE id = @UserId)
            RETURN 5060;

        UPDATE dbo.[User]
        SET passwordHash = @NewPasswordHash,
            updatedAt = GETDATE()
        WHERE id = @UserId;

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
