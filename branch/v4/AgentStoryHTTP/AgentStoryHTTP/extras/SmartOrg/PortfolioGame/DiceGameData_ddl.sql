
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiceGameData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DiceGameData](
	[id] [int] NOT NULL,
	[alias] [nvarchar](16) NOT NULL,
	[tx_id] [nvarchar](100) NOT NULL,
	[lastEditedDay] [int] NOT NULL,
	[lastEditedMonth] [int] NOT NULL,
	[lastEditedYear] [int] NOT NULL,
	[lastEditedWhen] [datetime] NOT NULL,
	
	[pearl] [int] NULL,
	[oyster] [int] NULL,
	[breadAndButter] [int] NULL,
	[whiteElephant] [int] NULL,
	
	[Funded_Success] [int] NULL,
	[Funded_Points] [int] NULL,
	
	[UnFunded_Points] [int] NULL,
	[UnFunded_Success] [int] NULL,
	
	[Best5_Success] [int] NULL,
	[Best5_Points] [int] NULL,
	
 CONSTRAINT [PK_DiceGameData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE UNIQUE NONCLUSTERED INDEX IX_DiceGameData ON dbo.DiceGameData
	(
	tx_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.DiceGameData SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
