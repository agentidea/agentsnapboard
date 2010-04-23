IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TackDieGameData]') AND type in (N'U'))
DROP TABLE [dbo].[TackDieGameData]
GO



SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TackDieGameData](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[alias] [nvarchar](100) NOT NULL,
	[tx_id] [nvarchar](100) NOT NULL,
	[lastEditedDay] [int] NOT NULL,
	[lastEditedMonth] [int] NOT NULL,
	[lastEditedYear] [int] NOT NULL,
	[lastEditedWhen] [datetime] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	

	[InitialProbability] int NULL,
	[FinalProbability] int NULL,
	[YourCall] int NULL,
	[TackOrient]  int NULL,
	[WillInvestFurther]  int NULL,
	[DieOrient]  int NULL,
	--[OrderToMarket]  int NULL,


	
 CONSTRAINT [PK_TackDieGameData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

ALTER TABLE [dbo].[TackDieGameData] ADD  DEFAULT (newid()) FOR [guid]
GO

ALTER TABLE dbo.TackDieGameData ADD CONSTRAINT
	IX_TackDieGameData_TX_ID UNIQUE NONCLUSTERED 
	(
	tx_id
	) WITH(  IGNORE_DUP_KEY = OFF )

GO
