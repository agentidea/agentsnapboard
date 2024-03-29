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
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SposorSelf]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_SposorSelf]
GO
USE [AgentStory]
GO
/****** Object:  Table [dbo].[Invitation]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Invitation]') AND type in (N'U'))
DROP TABLE [dbo].[Invitation]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Page]') AND type in (N'U'))
DROP TABLE [dbo].[Page]
GO
/****** Object:  Table [dbo].[PageElement]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElement]') AND type in (N'U'))
DROP TABLE [dbo].[PageElement]
GO
/****** Object:  Table [dbo].[PageElementMap]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElementMap]') AND type in (N'U'))
DROP TABLE [dbo].[PageElementMap]
GO
/****** Object:  Table [dbo].[PageElementType]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElementType]') AND type in (N'U'))
DROP TABLE [dbo].[PageElementType]
GO
/****** Object:  Table [dbo].[PagePageElementMap]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PagePageElementMap]') AND type in (N'U'))
DROP TABLE [dbo].[PagePageElementMap]
GO
/****** Object:  Table [dbo].[Story]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Story]') AND type in (N'U'))
DROP TABLE [dbo].[Story]
GO
/****** Object:  Table [dbo].[StoryPage]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryPage]') AND type in (N'U'))
DROP TABLE [dbo].[StoryPage]
GO
/****** Object:  Table [dbo].[StoryPageElement]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryPageElement]') AND type in (N'U'))
DROP TABLE [dbo].[StoryPageElement]
GO
/****** Object:  Table [dbo].[StoryState]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryState]') AND type in (N'U'))
DROP TABLE [dbo].[StoryState]
GO
/****** Object:  Table [dbo].[StoryView]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryView]') AND type in (N'U'))
DROP TABLE [dbo].[StoryView]
GO
/****** Object:  Table [dbo].[StoryViewElement]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryViewElement]') AND type in (N'U'))
DROP TABLE [dbo].[StoryViewElement]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/05/2006 08:42:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]