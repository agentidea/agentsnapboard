IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DMData]') AND type in (N'U'))
DROP TABLE [dbo].[DMData]
GO



SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DMData](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[owner_id] int NOT NULL,
	[Day] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Minute] [int] NOT NULL,
	[Hour] [int] NOT NULL,
	[When] [datetime] NOT NULL,
	[Ticks] [bigint] NOT NULL,
	
	 sugar 	int				NULL,
	 insulinA 	int			NULL,
	 insulinB	int		 NULL,
	 carbs int NULL,
	 comment 	ntext			NULL,
	


	
 CONSTRAINT [PK_DMData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

ALTER TABLE dbo.DMData ADD CONSTRAINT
	FK_DMData_Users FOREIGN KEY
	(
	owner_id
	) REFERENCES dbo.Users
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
