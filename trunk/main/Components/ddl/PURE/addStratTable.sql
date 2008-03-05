USE [AgentStory]
GO
/****** Object:  Table [dbo].[StrategyTable]    Script Date: 08/14/2007 17:50:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StrategyTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[stJSON64] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateLastModified] [datetime] NOT NULL,
	[owner_id] [int] NOT NULL,
	[name64] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_StrategyTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [AgentStory]
GO
ALTER TABLE [dbo].[StrategyTable]  WITH CHECK ADD  CONSTRAINT [FK_StrategyTable_Users] FOREIGN KEY([owner_id])
REFERENCES [dbo].[Users] ([id])