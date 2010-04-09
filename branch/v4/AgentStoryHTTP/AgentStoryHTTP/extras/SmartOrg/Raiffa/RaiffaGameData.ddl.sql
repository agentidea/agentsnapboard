IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RaiffaGameData]') AND type in (N'U'))
DROP TABLE [dbo].[RaiffaGameData]
GO



/****** Object:  Table [dbo].[RaiffaGameData]    Script Date: 04/05/2010 13:37:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RaiffaGameData](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[alias] [nvarchar](100) NOT NULL,
	[tx_id] [nvarchar](100) NOT NULL,
	[lastEditedDay] [int] NOT NULL,
	[lastEditedMonth] [int] NOT NULL,
	[lastEditedYear] [int] NOT NULL,
	[lastEditedWhen] [datetime] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	
	[AttilaHunDied_HIGH] [int] NULL,
	[AttilaHunDied_LOW] [int] NULL,
	[USAutoThefts_HIGH] [int] NULL,
	[USAutoThefts_LOW] [int] NULL,
	[USBeefConsumption_HIGH] [int] NULL,
	[USBeefConsumption_LOW] [int] NULL,
	[BeijingToMoscow_HIGH] [int] NULL,
	[BeijingToMoscow_LOW] [int] NULL,
	[PlutoOrbit_HIGH] [float] NULL,
	[PlutoOrbit_LOW] [float] NULL,
	
 CONSTRAINT [PK_reports] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

ALTER TABLE [dbo].[RaiffaGameData] ADD  DEFAULT (newid()) FOR [guid]
GO

ALTER TABLE dbo.RaiffaGameData ADD CONSTRAINT
	IX_RaiffaGameData_TX_ID UNIQUE NONCLUSTERED 
	(
	tx_id
	) WITH(  IGNORE_DUP_KEY = OFF )

GO