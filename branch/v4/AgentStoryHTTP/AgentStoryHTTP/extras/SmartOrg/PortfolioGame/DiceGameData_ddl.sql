
CREATE TABLE [dbo].[DiceGameData](
	[id] [int] NOT NULL,
	[alias] [nvarchar](16) NOT NULL,
	[tx_id] [nvarchar](100) NOT NULL,
	[lastEditedDay] [int] NOT NULL,
	[lastEditedMonth] [int] NOT NULL,
	[lastEditedYear] [int] NOT NULL,
	[lastEditedWhen] [datetime] NOT NULL,
	
	[guid] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	
	[pearl] [int] NOT NULL DEFAULT(-1),
	[oyster] [int] NOT NULL DEFAULT(-1),
	[breadAndButter] [int] NOT NULL DEFAULT(-1),
	[whiteElephant] [int] NOT NULL DEFAULT(-1),
	
	[Funded_Success] [int] NOT NULL DEFAULT(-1),
	[Funded_Points] [int] NOT NULL DEFAULT(-1),
	
	[UnFunded_Points] [int] NOT NULL DEFAULT(-1),
	[UnFunded_Success] [int] NOT NULL DEFAULT(-1),
	
	[Best5_Success] [int] NOT NULL DEFAULT(-1),
	[Best5_Points] [int] NOT NULL DEFAULT(-1),
	
 CONSTRAINT [PK_DiceGameData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_DiceGameData ON dbo.DiceGameData
	(
	tx_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.DiceGameData SET (LOCK_ESCALATION = TABLE)
GO

