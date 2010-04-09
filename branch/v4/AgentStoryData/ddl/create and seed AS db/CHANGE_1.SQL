/*
-- mods to existing db for upgrade from basic slide navigator
ALTER TABLE dbo.Story ADD
	IncludeCodeDirName nvarchar(64) NULL
GO

*/


ALTER TABLE dbo.StoryTuple
	DROP CONSTRAINT FK_StoryTuple_Tuple
GO
ALTER TABLE dbo.StoryTuple
	DROP CONSTRAINT FK_StoryTuple_Story
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tuple]') AND type in (N'U'))
DROP TABLE [dbo].[Tuple]
GO

CREATE TABLE [dbo].[Tuple]
(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[code] [nvarchar](100) NOT NULL,
	[description] [nvarchar](254) NULL,
	[units] [nvarchar](100) NULL,
	[guid] [uniqueidentifier] DEFAULT (newid()) NOT NULL,
	[value] ntext NULL,
	[numValue] decimal(18,4) NULL,
	
	CONSTRAINT [PK_Tuple] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)

)

GO


CREATE UNIQUE NONCLUSTERED INDEX IX_TupleGUID ON [dbo].[Tuple]
(
	guid
) 
GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryTuple]') AND type in (N'U'))
DROP TABLE [dbo].[StoryTuple]
GO

CREATE TABLE dbo.StoryTuple
	(
	StoryID int NOT NULL,
	TupleID int NOT NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.StoryTuple ADD CONSTRAINT
	PK_StoryTuple PRIMARY KEY CLUSTERED 
	(
	StoryID,
	TupleID
	) 

GO

ALTER TABLE dbo.StoryTuple ADD CONSTRAINT
	FK_StoryTuple_Tuple FOREIGN KEY
	(
	TupleID
	) REFERENCES dbo.Tuple
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.StoryTuple ADD CONSTRAINT
	FK_StoryTuple_Story FOREIGN KEY
	(
	StoryID
	) REFERENCES dbo.Story
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[readTuplesByStory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[readTuplesByStory]
GO

CREATE PROCEDURE readTuplesByStory
	@storyID int
AS
BEGIN
	select * from vStoryTuples where storyID = @storyID
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddTuple]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddTuple]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[updateTuple]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[updateTuple]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[readTuplesByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[readTuplesByID]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[addTuple]
	@name nvarchar(100) ,
	@code nvarchar(100) ,
	@description nvarchar(254) = null,
	@guid uniqueidentifier,
	@units nvarchar(100),
	@value ntext = null,
	@numValue decimal(18,4) = null,
	@pk int OUTPUT
	
AS
BEGIN


	INSERT INTO [dbo].[Tuple]
           ([name]
           ,[code]
           ,[description]
           ,[units]
           ,[guid]
           ,[value]
           ,[numValue])
     VALUES
	(
		@name,
		@code,
		@description,
		@units,
		@guid,
		@value,
		@numValue
	)
   
   set @pk = @@IDENTITY

   return @pk
   
   
   
           
END

GO


CREATE PROCEDURE updateTuple
	@tupleID int,
	@name nvarchar(100) ,
	@code nvarchar(100) ,
	@description nvarchar(254),
	@units nvarchar(100),
	@value ntext,
	@numValue decimal (18,4)
AS
BEGIN
	UPDATE [dbo].[Tuple]
	SET [name] = @name
	,[code] = @code
	,[description] = @description
	,[units] = @units
	,[value] = @value
	,[numValue] = @numValue
	WHERE id = @tupleID
END
GO

CREATE PROCEDURE [dbo].[readTuplesByID]
	@tupleID int
AS
BEGIN
	select * from vStoryTuples where id = @tupleID
END

GO