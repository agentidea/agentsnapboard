USE [AgentStory]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryView_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[FACT_StoryUserView]'))
ALTER TABLE [dbo].[FACT_StoryUserView] DROP CONSTRAINT [FK_StoryView_Story]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryView_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[FACT_StoryUserView]'))
ALTER TABLE [dbo].[FACT_StoryUserView] DROP CONSTRAINT [FK_StoryView_Users]
GO
USE [AgentStory]
GO
/****** Object:  Table [dbo].[FACT_StoryUserView]    Script Date: 01/29/2007 22:03:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FACT_StoryUserView]') AND type in (N'U'))
DROP TABLE [dbo].[FACT_StoryUserView]

USE [AgentStory]
GO
/****** Object:  Table [dbo].[FACT_StoryView]    Script Date: 01/29/2007 22:04:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FACT_StoryView]') AND type in (N'U'))
DROP TABLE [dbo].[FACT_StoryView]

USE [AgentStory]
GO
/****** Object:  Table [dbo].[emailMessageStates]    Script Date: 01/29/2007 22:06:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[emailMessageStates]') AND type in (N'U'))
DROP TABLE [dbo].[emailMessageStates]


USE [AgentStory]
GO
/****** Object:  Table [dbo].[emailMessage]    Script Date: 01/29/2007 22:07:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[emailMessage]') AND type in (N'U'))
DROP TABLE [dbo].[emailMessage]


USE [AgentStory]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invitation_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invitation]'))
ALTER TABLE [dbo].[Invitation] DROP CONSTRAINT [FK_Invitation_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invitation_Users1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invitation]'))
ALTER TABLE [dbo].[Invitation] DROP CONSTRAINT [FK_Invitation_Users1]
GO
USE [AgentStory]
GO
/****** Object:  Table [dbo].[Invitation]    Script Date: 01/29/2007 22:07:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Invitation]') AND type in (N'U'))
DROP TABLE [dbo].[Invitation]

USE [AgentStory]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupEditors_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupEditors]'))
ALTER TABLE [dbo].[StoryGroupEditors] DROP CONSTRAINT [FK_StoryGroupEditors_Groups]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupEditors_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupEditors]'))
ALTER TABLE [dbo].[StoryGroupEditors] DROP CONSTRAINT [FK_StoryGroupEditors_Story]
GO
USE [AgentStory]
GO
/****** Object:  Table [dbo].[StoryGroupEditors]    Script Date: 01/29/2007 22:08:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryGroupEditors]') AND type in (N'U'))
DROP TABLE [dbo].[StoryGroupEditors]

USE [AgentStory]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupViewers_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupViewers]'))
ALTER TABLE [dbo].[StoryGroupViewers] DROP CONSTRAINT [FK_StoryGroupViewers_Groups]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupViewers_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupViewers]'))
ALTER TABLE [dbo].[StoryGroupViewers] DROP CONSTRAINT [FK_StoryGroupViewers_Story]
GO
USE [AgentStory]
GO
/****** Object:  Table [dbo].[StoryGroupViewers]    Script Date: 01/29/2007 22:09:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryGroupViewers]') AND type in (N'U'))
DROP TABLE [dbo].[StoryGroupViewers]

USE [AgentStory]
GO
/****** Object:  Table [dbo].[SystemLog]    Script Date: 01/29/2007 22:10:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemLog]') AND type in (N'U'))
DROP TABLE [dbo].[SystemLog]


USE [AgentStory]
GO
/****** Object:  Table [dbo].[vEmailMessage]    Script Date: 01/29/2007 22:10:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vEmailMessage]') AND type in (N'U'))
DROP TABLE [dbo].[vEmailMessage]

USE [AgentStory]
GO
/****** Object:  Table [dbo].[vStoryPlatform]    Script Date: 01/29/2007 22:10:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vStoryPlatform]') AND type in (N'U'))
DROP TABLE [dbo].[vStoryPlatform]


USE [AgentStory]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryPage_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryPage]'))
ALTER TABLE [dbo].[StoryPage] DROP CONSTRAINT [FK_StoryPage_Page]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryPage_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryPage]'))
ALTER TABLE [dbo].[StoryPage] DROP CONSTRAINT [FK_StoryPage_Story]
GO
USE [AgentStory]
GO
/****** Object:  Table [dbo].[StoryPage]    Script Date: 01/29/2007 22:14:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryPage]') AND type in (N'U'))


USE [AgentStory]
GO
/****** Object:  Table [dbo].[StoryState]    Script Date: 01/29/2007 22:13:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryState]') AND type in (N'U'))
DROP TABLE [dbo].[StoryState]