USE [AgentStory]
GO
/****** Object:  Table [dbo].[Invitation]    Script Date: 12/05/2006 08:41:47 ******/
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
 CONSTRAINT [PK_Invitation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Page]    Script Date: 12/05/2006 08:41:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[seq] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[gridX] [int] NOT NULL,
	[gridY] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[user_id_originator] [int] NOT NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PageElement]    Script Date: 12/05/2006 08:41:48 ******/
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
/****** Object:  Table [dbo].[PageElementMap]    Script Date: 12/05/2006 08:41:48 ******/
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
 CONSTRAINT [PK_PageElementMap] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PageElementType]    Script Date: 12/05/2006 08:41:48 ******/
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
/****** Object:  Table [dbo].[PagePageElementMap]    Script Date: 12/05/2006 08:41:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PagePageElementMap](
	[page_id] [int] NOT NULL,
	[pageElementMap_id] [int] NOT NULL,
 CONSTRAINT [PK_PagePageElementMap] PRIMARY KEY CLUSTERED 
(
	[page_id] ASC,
	[pageElementMap_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Story]    Script Date: 12/05/2006 08:41:48 ******/
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_UniqueStoryForAuthor] UNIQUE NONCLUSTERED 
(
	[title] ASC,
	[user_id_originator] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoryPage]    Script Date: 12/05/2006 08:41:48 ******/
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
/****** Object:  Table [dbo].[StoryPageElement]    Script Date: 12/05/2006 08:41:48 ******/
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
/****** Object:  Table [dbo].[StoryState]    Script Date: 12/05/2006 08:41:48 ******/
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
/****** Object:  Table [dbo].[StoryView]    Script Date: 12/05/2006 08:41:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoryView](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoryViewElement]    Script Date: 12/05/2006 08:41:48 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 12/05/2006 08:41:48 ******/
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
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_OrigInviteCode] UNIQUE NONCLUSTERED 
(
	[OrigInvitationCode] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

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
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Users] FOREIGN KEY([user_id_to])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Users1] FOREIGN KEY([user_id_from])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_Users] FOREIGN KEY([user_id_originator])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[PageElement]  WITH CHECK ADD  CONSTRAINT [FK_PageElement_PageElementType] FOREIGN KEY([typeID])
REFERENCES [dbo].[PageElementType] ([id])
GO
ALTER TABLE [dbo].[PageElement]  WITH CHECK ADD  CONSTRAINT [FK_PageElement_Users] FOREIGN KEY([user_id_originator])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[PageElementMap]  WITH CHECK ADD  CONSTRAINT [FK_PageElementMap_PageElement] FOREIGN KEY([pageElement_id])
REFERENCES [dbo].[PageElement] ([id])
GO
ALTER TABLE [dbo].[PagePageElementMap]  WITH CHECK ADD  CONSTRAINT [FK_PagePageElementMap_Page] FOREIGN KEY([page_id])
REFERENCES [dbo].[Page] ([id])
GO
ALTER TABLE [dbo].[PagePageElementMap]  WITH CHECK ADD  CONSTRAINT [FK_PagePageElementMap_PageElementMap] FOREIGN KEY([pageElementMap_id])
REFERENCES [dbo].[PageElementMap] ([id])
GO
ALTER TABLE [dbo].[Story]  WITH CHECK ADD  CONSTRAINT [FK_Story_StoryState] FOREIGN KEY([state])
REFERENCES [dbo].[StoryState] ([id])
GO
ALTER TABLE [dbo].[Story]  WITH CHECK ADD  CONSTRAINT [FK_Story_Users] FOREIGN KEY([user_id_originator])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[StoryPage]  WITH CHECK ADD  CONSTRAINT [FK_StoryPage_Page] FOREIGN KEY([page_id])
REFERENCES [dbo].[Page] ([id])
GO
ALTER TABLE [dbo].[StoryPage]  WITH CHECK ADD  CONSTRAINT [FK_StoryPage_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[StoryPageElement]  WITH CHECK ADD  CONSTRAINT [FK_StoryPageElement_PageElement] FOREIGN KEY([pageElement_id])
REFERENCES [dbo].[PageElement] ([id])
GO
ALTER TABLE [dbo].[StoryPageElement]  WITH CHECK ADD  CONSTRAINT [FK_StoryPageElement_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_SposorSelf] FOREIGN KEY([sponsorID])
REFERENCES [dbo].[Users] ([id])