USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_emailMessage_emailMessageStates]') AND parent_object_id = OBJECT_ID(N'[dbo].[emailMessage]'))
ALTER TABLE [dbo].[emailMessage] DROP CONSTRAINT [FK_emailMessage_emailMessageStates]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryView_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[FACT_StoryUserView]'))
ALTER TABLE [dbo].[FACT_StoryUserView] DROP CONSTRAINT [FK_StoryView_Story]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryView_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[FACT_StoryUserView]'))
ALTER TABLE [dbo].[FACT_StoryUserView] DROP CONSTRAINT [FK_StoryView_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invitation_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invitation]'))
ALTER TABLE [dbo].[Invitation] DROP CONSTRAINT [FK_Invitation_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invitation_Users1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invitation]'))
ALTER TABLE [dbo].[Invitation] DROP CONSTRAINT [FK_Invitation_Users1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Page]'))
ALTER TABLE [dbo].[Page] DROP CONSTRAINT [FK_Page_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElement_PageElementType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElement]'))
ALTER TABLE [dbo].[PageElement] DROP CONSTRAINT [FK_PageElement_PageElementType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElement_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElement]'))
ALTER TABLE [dbo].[PageElement] DROP CONSTRAINT [FK_PageElement_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElementMap_PageElement]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElementMap]'))
ALTER TABLE [dbo].[PageElementMap] DROP CONSTRAINT [FK_PageElementMap_PageElement]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PagePageElementMap_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[PagePageElementMap]'))
ALTER TABLE [dbo].[PagePageElementMap] DROP CONSTRAINT [FK_PagePageElementMap_Page]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PagePageElementMap_PageElementMap]') AND parent_object_id = OBJECT_ID(N'[dbo].[PagePageElementMap]'))
ALTER TABLE [dbo].[PagePageElementMap] DROP CONSTRAINT [FK_PagePageElementMap_PageElementMap]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Story_StoryState]') AND parent_object_id = OBJECT_ID(N'[dbo].[Story]'))
ALTER TABLE [dbo].[Story] DROP CONSTRAINT [FK_Story_StoryState]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Story_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Story]'))
ALTER TABLE [dbo].[Story] DROP CONSTRAINT [FK_Story_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupEditors_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupEditors]'))
ALTER TABLE [dbo].[StoryGroupEditors] DROP CONSTRAINT [FK_StoryGroupEditors_Groups]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupEditors_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupEditors]'))
ALTER TABLE [dbo].[StoryGroupEditors] DROP CONSTRAINT [FK_StoryGroupEditors_Story]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupViewers_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupViewers]'))
ALTER TABLE [dbo].[StoryGroupViewers] DROP CONSTRAINT [FK_StoryGroupViewers_Groups]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupViewers_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupViewers]'))
ALTER TABLE [dbo].[StoryGroupViewers] DROP CONSTRAINT [FK_StoryGroupViewers_Story]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryPage_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryPage]'))
ALTER TABLE [dbo].[StoryPage] DROP CONSTRAINT [FK_StoryPage_Page]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryPage_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryPage]'))
ALTER TABLE [dbo].[StoryPage] DROP CONSTRAINT [FK_StoryPage_Story]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryPageElement_PageElement]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryPageElement]'))
ALTER TABLE [dbo].[StoryPageElement] DROP CONSTRAINT [FK_StoryPageElement_PageElement]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryPageElement_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryPageElement]'))
ALTER TABLE [dbo].[StoryPageElement] DROP CONSTRAINT [FK_StoryPageElement_Story]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryUserEditors_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryUserEditors]'))
ALTER TABLE [dbo].[StoryUserEditors] DROP CONSTRAINT [FK_StoryUserEditors_Story]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryUserEditors_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryUserEditors]'))
ALTER TABLE [dbo].[StoryUserEditors] DROP CONSTRAINT [FK_StoryUserEditors_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryUserViewers_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryUserViewers]'))
ALTER TABLE [dbo].[StoryUserViewers] DROP CONSTRAINT [FK_StoryUserViewers_Story]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryUserViewers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryUserViewers]'))
ALTER TABLE [dbo].[StoryUserViewers] DROP CONSTRAINT [FK_StoryUserViewers_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SposorSelf]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_SposorSelf]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersGroups_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersGroups]'))
ALTER TABLE [dbo].[UsersGroups] DROP CONSTRAINT [FK_UsersGroups_Groups]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersGroups_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersGroups]'))
ALTER TABLE [dbo].[UsersGroups] DROP CONSTRAINT [FK_UsersGroups_Users]
GO
USE [AgentStory]
GO
/****** Object:  Table [dbo].[emailMessage]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[emailMessage]') AND type in (N'U'))
DROP TABLE [dbo].[emailMessage]
GO
/****** Object:  Table [dbo].[emailMessageStates]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[emailMessageStates]') AND type in (N'U'))
DROP TABLE [dbo].[emailMessageStates]
GO
/****** Object:  Table [dbo].[FACT_StoryUserView]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FACT_StoryUserView]') AND type in (N'U'))
DROP TABLE [dbo].[FACT_StoryUserView]
GO
/****** Object:  Table [dbo].[FACT_StoryView]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FACT_StoryView]') AND type in (N'U'))
DROP TABLE [dbo].[FACT_StoryView]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Groups]') AND type in (N'U'))
DROP TABLE [dbo].[Groups]
GO
/****** Object:  Table [dbo].[Invitation]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Invitation]') AND type in (N'U'))
DROP TABLE [dbo].[Invitation]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Page]') AND type in (N'U'))
DROP TABLE [dbo].[Page]
GO
/****** Object:  Table [dbo].[PageElement]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElement]') AND type in (N'U'))
DROP TABLE [dbo].[PageElement]
GO
/****** Object:  Table [dbo].[PageElementMap]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElementMap]') AND type in (N'U'))
DROP TABLE [dbo].[PageElementMap]
GO
/****** Object:  Table [dbo].[PageElementType]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElementType]') AND type in (N'U'))
DROP TABLE [dbo].[PageElementType]
GO
/****** Object:  Table [dbo].[PagePageElementMap]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PagePageElementMap]') AND type in (N'U'))
DROP TABLE [dbo].[PagePageElementMap]
GO
/****** Object:  Table [dbo].[Story]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Story]') AND type in (N'U'))
DROP TABLE [dbo].[Story]
GO
/****** Object:  Table [dbo].[StoryGroupEditors]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryGroupEditors]') AND type in (N'U'))
DROP TABLE [dbo].[StoryGroupEditors]
GO
/****** Object:  Table [dbo].[StoryGroupViewers]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryGroupViewers]') AND type in (N'U'))
DROP TABLE [dbo].[StoryGroupViewers]
GO
/****** Object:  Table [dbo].[StoryPage]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryPage]') AND type in (N'U'))
DROP TABLE [dbo].[StoryPage]
GO
/****** Object:  Table [dbo].[StoryPageElement]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryPageElement]') AND type in (N'U'))
DROP TABLE [dbo].[StoryPageElement]
GO
/****** Object:  Table [dbo].[StoryState]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryState]') AND type in (N'U'))
DROP TABLE [dbo].[StoryState]
GO
/****** Object:  Table [dbo].[StoryUserEditors]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryUserEditors]') AND type in (N'U'))
DROP TABLE [dbo].[StoryUserEditors]
GO
/****** Object:  Table [dbo].[StoryUserViewers]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryUserViewers]') AND type in (N'U'))
DROP TABLE [dbo].[StoryUserViewers]
GO
/****** Object:  Table [dbo].[StoryViewElement]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryViewElement]') AND type in (N'U'))
DROP TABLE [dbo].[StoryViewElement]
GO
/****** Object:  Table [dbo].[SystemLog]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemLog]') AND type in (N'U'))
DROP TABLE [dbo].[SystemLog]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[UsersGroups]    Script Date: 01/30/2007 06:21:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersGroups]') AND type in (N'U'))
DROP TABLE [dbo].[UsersGroups]

USE [AgentStory]
GO
/****** Object:  View [dbo].[vEmailMessage]    Script Date: 01/30/2007 06:24:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vEmailMessage]'))
DROP VIEW [dbo].[vEmailMessage]
GO
/****** Object:  View [dbo].[vStoryPlatform]    Script Date: 01/30/2007 06:24:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vStoryPlatform]'))
DROP VIEW [dbo].[vStoryPlatform]