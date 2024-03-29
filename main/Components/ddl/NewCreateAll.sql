USE [master]
GO
/****** Object:  Database [ AgentStory_USF ]    Script Date: 06/24/2007 06:41:49 ******/
CREATE DATABASE [AgentStory_USF] ON  PRIMARY 
( NAME = N'AgentStory_USF', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\AgentStory_USF.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AgentStory_USF_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\AgentStory_USF_log.LDF' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'AgentStory_USF', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AgentStory_USF].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AgentStory_USF] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AgentStory_USF] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AgentStory_USF] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AgentStory_USF] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AgentStory_USF] SET ARITHABORT OFF 
GO
ALTER DATABASE [AgentStory_USF] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [AgentStory_USF] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [AgentStory_USF] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AgentStory_USF] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AgentStory_USF] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AgentStory_USF] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AgentStory_USF] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AgentStory_USF] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AgentStory_USF] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AgentStory_USF] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AgentStory_USF] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AgentStory_USF] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AgentStory_USF] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AgentStory_USF] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AgentStory_USF] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AgentStory_USF] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AgentStory_USF] SET  READ_WRITE 
GO
ALTER DATABASE [AgentStory_USF] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AgentStory_USF] SET  MULTI_USER 
GO
ALTER DATABASE [AgentStory_USF] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AgentStory_USF] SET DB_CHAINING OFF 
GO

USE [AgentStory_USF]
GO
/****** Object:  Table [dbo].[emailMessage]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[emailMessage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[subject] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[to] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[from] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[body] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[state] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[userAddedID] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[reply_to] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[lastModified] [datetime] NOT NULL,
	[lastError] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_emailMessage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[emailMessageStates]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[emailMessageStates](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_emailMessageStates] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FACT_StoryUserView]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACT_StoryUserView](
	[user_id] [int] NOT NULL,
	[story_id] [int] NOT NULL,
	[rating] [int] NOT NULL,
	[views] [int] NOT NULL,
 CONSTRAINT [PK_StoryView] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[story_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FACT_StoryView]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACT_StoryView](
	[story_id] [int] NOT NULL,
	[rating] [int] NOT NULL,
	[views] [int] NOT NULL,
	[lastEditedBy] [int] NULL,
	[lastEditedWhen] [datetime] NULL,
 CONSTRAINT [PK_FACT_StoryView] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Groups]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[groupStartedBy] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[description] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invitation]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invitation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[viewURL] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[imgURL] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[invitationCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[invitationText] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[user_id_to] [int] NOT NULL,
	[user_id_from] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Invitation_guid]  DEFAULT (newid()),
	[title] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[InviteEvent] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[lastError] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Invitation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Page]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[name] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[seq] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[gridX] [int] NOT NULL,
	[gridY] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[user_id_originator] [int] NOT NULL,
	[gridZ] [int] NOT NULL,
	[style] [int] NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PageElement]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageElement](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[typeID] [int] NOT NULL,
	[value] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[tags] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[user_id_originator] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_PageElement] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PageElementMap]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageElementMap](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[pageElement_id] [int] NOT NULL,
	[gridX] [int] NOT NULL,
	[gridY] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[gridZ] [int] NULL,
	[user_id_originator] [int] NULL,
	[lastModified] [datetime] NULL,
 CONSTRAINT [PK_PageElementMap] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PageElementType]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageElementType](
	[id] [int] NOT NULL,
	[code] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_PageElementType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PagePageElementMap]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PagePageElementMap](
	[page_id] [int] NOT NULL,
	[pageElementMap_id] [int] NOT NULL,
	[sequence] [int] NULL,
 CONSTRAINT [PK_PagePageElementMap] PRIMARY KEY CLUSTERED 
(
	[page_id] ASC,
	[pageElementMap_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Story]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Story](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[user_id_originator] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Story_guid]  DEFAULT (newid()),
	[viewableBy] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[editableBy] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[description] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[state] [int] NOT NULL CONSTRAINT [DF_Story_state]  DEFAULT ((0)),
 CONSTRAINT [PK_Story] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoryGroupEditors]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoryGroupEditors](
	[story_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_StoryGroupEditors] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[group_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoryGroupViewers]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoryGroupViewers](
	[story_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_StoryGroupViewers] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[group_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoryPage]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoryPage](
	[story_id] [int] NOT NULL,
	[page_id] [int] NOT NULL,
 CONSTRAINT [PK_StoryPage] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[page_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoryPageElement]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoryPageElement](
	[story_id] [int] NOT NULL,
	[pageElement_id] [int] NOT NULL,
 CONSTRAINT [PK_StoryPageElement] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[pageElement_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoryState]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoryState](
	[id] [int] NOT NULL,
	[stateName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_StoryState] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoryUserEditors]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoryUserEditors](
	[story_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_StoryUserEditors] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[user_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoryUserViewers]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoryUserViewers](
	[story_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_StoryUserViewers] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[user_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoryViewElement]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoryViewElement](
	[story_view_id] [int] NOT NULL,
	[element_id] [int] NOT NULL,
	[sequence_number] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemLog]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemLog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[msg] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[lastName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[username] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[password] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[nick] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[email] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[roles] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[state] [tinyint] NOT NULL,
	[OrigInvitationCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[dateActivated] [datetime] NULL,
	[dateLastActive] [datetime] NULL,
	[pendingGUID] [uniqueidentifier] NULL,
	[userGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Users_userGUID]  DEFAULT (newid()),
	[sponsorID] [int] NULL,
	[notificationFrequency] [int] NOT NULL,
	[notificationTypes] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[tags] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_OrigInviteCode] UNIQUE NONCLUSTERED 
(
	[OrigInvitationCode] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersGroups]    Script Date: 06/24/2007 06:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersGroups](
	[user_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_UsersGroups] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[group_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
USE [AgentStory_USF]
GO
ALTER TABLE [dbo].[emailMessage]  WITH CHECK ADD  CONSTRAINT [FK_emailMessage_emailMessageStates] FOREIGN KEY([state])
REFERENCES [dbo].[emailMessageStates] ([id])
GO
ALTER TABLE [dbo].[FACT_StoryUserView]  WITH CHECK ADD  CONSTRAINT [FK_StoryView_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[FACT_StoryUserView]  WITH CHECK ADD  CONSTRAINT [FK_StoryView_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Users] FOREIGN KEY([user_id_to])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Users1] FOREIGN KEY([user_id_from])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Page]  WITH NOCHECK ADD  CONSTRAINT [FK_Page_Users] FOREIGN KEY([user_id_originator])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Users]
GO
ALTER TABLE [dbo].[PageElement]  WITH NOCHECK ADD  CONSTRAINT [FK_PageElement_PageElementType] FOREIGN KEY([typeID])
REFERENCES [dbo].[PageElementType] ([id])
GO
ALTER TABLE [dbo].[PageElement] CHECK CONSTRAINT [FK_PageElement_PageElementType]
GO
ALTER TABLE [dbo].[PageElement]  WITH NOCHECK ADD  CONSTRAINT [FK_PageElement_Users] FOREIGN KEY([user_id_originator])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[PageElement] CHECK CONSTRAINT [FK_PageElement_Users]
GO
ALTER TABLE [dbo].[PageElementMap]  WITH NOCHECK ADD  CONSTRAINT [FK_PageElementMap_PageElement] FOREIGN KEY([pageElement_id])
REFERENCES [dbo].[PageElement] ([id])
GO
ALTER TABLE [dbo].[PageElementMap] CHECK CONSTRAINT [FK_PageElementMap_PageElement]
GO
ALTER TABLE [dbo].[PagePageElementMap]  WITH NOCHECK ADD  CONSTRAINT [FK_PagePageElementMap_Page] FOREIGN KEY([page_id])
REFERENCES [dbo].[Page] ([id])
GO
ALTER TABLE [dbo].[PagePageElementMap] CHECK CONSTRAINT [FK_PagePageElementMap_Page]
GO
ALTER TABLE [dbo].[PagePageElementMap]  WITH NOCHECK ADD  CONSTRAINT [FK_PagePageElementMap_PageElementMap] FOREIGN KEY([pageElementMap_id])
REFERENCES [dbo].[PageElementMap] ([id])
GO
ALTER TABLE [dbo].[PagePageElementMap] CHECK CONSTRAINT [FK_PagePageElementMap_PageElementMap]
GO
ALTER TABLE [dbo].[Story]  WITH NOCHECK ADD  CONSTRAINT [FK_Story_StoryState] FOREIGN KEY([state])
REFERENCES [dbo].[StoryState] ([id])
GO
ALTER TABLE [dbo].[Story] CHECK CONSTRAINT [FK_Story_StoryState]
GO
ALTER TABLE [dbo].[Story]  WITH NOCHECK ADD  CONSTRAINT [FK_Story_Users] FOREIGN KEY([user_id_originator])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Story] CHECK CONSTRAINT [FK_Story_Users]
GO
ALTER TABLE [dbo].[StoryGroupEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupEditors_Groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
ALTER TABLE [dbo].[StoryGroupEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupEditors_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[StoryGroupViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupViewers_Groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
ALTER TABLE [dbo].[StoryGroupViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupViewers_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[StoryPage]  WITH NOCHECK ADD  CONSTRAINT [FK_StoryPage_Page] FOREIGN KEY([page_id])
REFERENCES [dbo].[Page] ([id])
GO
ALTER TABLE [dbo].[StoryPage] CHECK CONSTRAINT [FK_StoryPage_Page]
GO
ALTER TABLE [dbo].[StoryPage]  WITH NOCHECK ADD  CONSTRAINT [FK_StoryPage_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[StoryPage] CHECK CONSTRAINT [FK_StoryPage_Story]
GO
ALTER TABLE [dbo].[StoryPageElement]  WITH NOCHECK ADD  CONSTRAINT [FK_StoryPageElement_PageElement] FOREIGN KEY([pageElement_id])
REFERENCES [dbo].[PageElement] ([id])
GO
ALTER TABLE [dbo].[StoryPageElement] CHECK CONSTRAINT [FK_StoryPageElement_PageElement]
GO
ALTER TABLE [dbo].[StoryPageElement]  WITH NOCHECK ADD  CONSTRAINT [FK_StoryPageElement_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[StoryPageElement] CHECK CONSTRAINT [FK_StoryPageElement_Story]
GO
ALTER TABLE [dbo].[StoryUserEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserEditors_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[StoryUserEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserEditors_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[StoryUserViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserViewers_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[StoryUserViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserViewers_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_SposorSelf] FOREIGN KEY([sponsorID])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[UsersGroups]  WITH CHECK ADD  CONSTRAINT [FK_UsersGroups_Groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
ALTER TABLE [dbo].[UsersGroups]  WITH CHECK ADD  CONSTRAINT [FK_UsersGroups_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])



USE [AgentStory_USF]
GO
/****** Object:  View [dbo].[vEmailMessage]    Script Date: 06/24/2007 06:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vEmailMessage]
AS
SELECT     dbo.emailMessage.id, dbo.emailMessage.subject, dbo.emailMessage.[to] AS 'toAddress', dbo.emailMessage.[from] AS 'fromAddress', 
                      dbo.emailMessage.body, dbo.emailMessageStates.name AS 'stateHR', dbo.emailMessage.state, dbo.emailMessage.dateAdded, 
                      dbo.emailMessage.userAddedID, dbo.emailMessage.guid, dbo.emailMessage.reply_to, dbo.emailMessage.lastModified, 
                      dbo.Users.firstName AS 'toFirstName', dbo.Users.lastName AS 'toLastName', dbo.Users.email, dbo.Users.OrigInvitationCode
FROM         dbo.emailMessage INNER JOIN
                      dbo.emailMessageStates ON dbo.emailMessage.state = dbo.emailMessageStates.id INNER JOIN
                      dbo.Users ON dbo.emailMessage.userAddedID = dbo.Users.id

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[56] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "emailMessage"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 289
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "emailMessageStates"
            Begin Extent = 
               Top = 152
               Left = 235
               Bottom = 230
               Right = 386
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 75
               Left = 519
               Bottom = 340
               Right = 690
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 18
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vEmailMessage'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vEmailMessage'

GO
/****** Object:  View [dbo].[vStoryPlatform]    Script Date: 06/24/2007 06:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vStoryPlatform]
AS
SELECT     dbo.Story.title, dbo.FACT_StoryView.rating, dbo.FACT_StoryView.views, dbo.FACT_StoryView.lastEditedBy, dbo.FACT_StoryView.lastEditedWhen, 
                      dbo.FACT_StoryView.story_id, dbo.Story.state
FROM         dbo.FACT_StoryView INNER JOIN
                      dbo.Story ON dbo.FACT_StoryView.story_id = dbo.Story.id

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[22] 4[4] 2[13] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "FACT_StoryView"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 193
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Story"
            Begin Extent = 
               Top = 6
               Left = 231
               Bottom = 114
               Right = 398
            End
            DisplayFlags = 280
            TopColumn = 5
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vStoryPlatform'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vStoryPlatform'

