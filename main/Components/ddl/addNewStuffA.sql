USE [AgentStory]
GO
/****** Object:  Table [dbo].[StoryUserEditors]    Script Date: 12/24/2006 08:55:05 ******/
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
USE [AgentStory]
GO
ALTER TABLE [dbo].[StoryUserEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserEditors_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[StoryUserEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserEditors_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])

USE [AgentStory]
GO
/****** Object:  Table [dbo].[StoryUserViewers]    Script Date: 12/24/2006 08:55:45 ******/
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
USE [AgentStory]
GO
ALTER TABLE [dbo].[StoryUserViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserViewers_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[StoryUserViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserViewers_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])

USE [AgentStory]
GO
/****** Object:  Table [dbo].[UsersGroups]    Script Date: 12/24/2006 08:56:16 ******/
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
USE [AgentStory]
GO
ALTER TABLE [dbo].[UsersGroups]  WITH CHECK ADD  CONSTRAINT [FK_UsersGroups_Groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
ALTER TABLE [dbo].[UsersGroups]  WITH CHECK ADD  CONSTRAINT [FK_UsersGroups_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])

USE [AgentStory]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 12/24/2006 08:56:35 ******/
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
USE [AgentStory]
GO
/****** Object:  Table [dbo].[FACT_StoryView]    Script Date: 12/24/2006 08:57:12 ******/
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

USE [AgentStory]
GO
/****** Object:  Table [dbo].[FACT_StoryUserView]    Script Date: 12/24/2006 08:57:29 ******/
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
USE [AgentStory]
GO
ALTER TABLE [dbo].[FACT_StoryUserView]  WITH CHECK ADD  CONSTRAINT [FK_StoryView_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[FACT_StoryUserView]  WITH CHECK ADD  CONSTRAINT [FK_StoryView_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])

USE [AgentStory]
GO
/****** Object:  Table [dbo].[StoryGroupEditors]    Script Date: 12/24/2006 09:02:57 ******/
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
USE [AgentStory]
GO
ALTER TABLE [dbo].[StoryGroupEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupEditors_Groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
ALTER TABLE [dbo].[StoryGroupEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupEditors_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])


USE [AgentStory]
GO
/****** Object:  Table [dbo].[StoryGroupViewers]    Script Date: 12/24/2006 09:03:29 ******/
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
USE [AgentStory]
GO
ALTER TABLE [dbo].[StoryGroupViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupViewers_Groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
ALTER TABLE [dbo].[StoryGroupViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupViewers_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
