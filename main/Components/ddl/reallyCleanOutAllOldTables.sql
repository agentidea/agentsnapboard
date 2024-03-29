/*
   Tuesday, January 30, 20076:10:13 AM
   User: 
   Server: SMARTORG-Y3A56H
   Database: AgentStory
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.emailMessage
	DROP CONSTRAINT FK_emailMessage_emailMessageStates
GO
DROP TABLE dbo.emailMessageStates
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.emailMessage
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.FACT_StoryView
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.UsersGroups
	DROP CONSTRAINT FK_UsersGroups_Groups
GO
ALTER TABLE dbo.StoryGroupEditors
	DROP CONSTRAINT FK_StoryGroupEditors_Groups
GO
ALTER TABLE dbo.StoryGroupViewers
	DROP CONSTRAINT FK_StoryGroupViewers_Groups
GO
DROP TABLE dbo.Groups
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.PageElement
	DROP CONSTRAINT FK_PageElement_PageElementType
GO
DROP TABLE dbo.PageElementType
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Story
	DROP CONSTRAINT FK_Story_StoryState
GO
DROP TABLE dbo.StoryState
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.StoryViewElement
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Invitation
	DROP CONSTRAINT FK_Invitation_Users
GO
ALTER TABLE dbo.Invitation
	DROP CONSTRAINT FK_Invitation_Users1
GO
ALTER TABLE dbo.Story
	DROP CONSTRAINT FK_Story_Users
GO
ALTER TABLE dbo.UsersGroups
	DROP CONSTRAINT FK_UsersGroups_Users
GO
ALTER TABLE dbo.StoryUserViewers
	DROP CONSTRAINT FK_StoryUserViewers_Users
GO
ALTER TABLE dbo.StoryUserEditors
	DROP CONSTRAINT FK_StoryUserEditors_Users
GO
ALTER TABLE dbo.FACT_StoryUserView
	DROP CONSTRAINT FK_StoryView_Users
GO
ALTER TABLE dbo.Page
	DROP CONSTRAINT FK_Page_Users
GO
ALTER TABLE dbo.PageElement
	DROP CONSTRAINT FK_PageElement_Users
GO
DROP TABLE dbo.Users
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.PageElementMap
	DROP CONSTRAINT FK_PageElementMap_PageElement
GO
ALTER TABLE dbo.StoryPageElement
	DROP CONSTRAINT FK_StoryPageElement_PageElement
GO
DROP TABLE dbo.PageElement
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.PagePageElementMap
	DROP CONSTRAINT FK_PagePageElementMap_PageElementMap
GO
DROP TABLE dbo.PageElementMap
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.StoryPage
	DROP CONSTRAINT FK_StoryPage_Page
GO
ALTER TABLE dbo.PagePageElementMap
	DROP CONSTRAINT FK_PagePageElementMap_Page
GO
DROP TABLE dbo.Page
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.PagePageElementMap
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.StoryPageElement
	DROP CONSTRAINT FK_StoryPageElement_Story
GO
ALTER TABLE dbo.StoryPage
	DROP CONSTRAINT FK_StoryPage_Story
GO
ALTER TABLE dbo.StoryUserViewers
	DROP CONSTRAINT FK_StoryUserViewers_Story
GO
ALTER TABLE dbo.StoryUserEditors
	DROP CONSTRAINT FK_StoryUserEditors_Story
GO
ALTER TABLE dbo.StoryGroupEditors
	DROP CONSTRAINT FK_StoryGroupEditors_Story
GO
ALTER TABLE dbo.StoryGroupViewers
	DROP CONSTRAINT FK_StoryGroupViewers_Story
GO
ALTER TABLE dbo.FACT_StoryUserView
	DROP CONSTRAINT FK_StoryView_Story
GO
DROP TABLE dbo.Story
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.FACT_StoryUserView
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.StoryGroupViewers
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.StoryGroupEditors
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.StoryUserEditors
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.StoryUserViewers
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.StoryPage
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.StoryPageElement
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.Invitation
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.UsersGroups
GO
COMMIT
