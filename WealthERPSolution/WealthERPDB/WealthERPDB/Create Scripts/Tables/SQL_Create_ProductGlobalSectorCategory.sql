USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductGlobalSectorCategory]    Script Date: 06/12/2009 18:19:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductGlobalSectorCategory](
	[PGSC_SectorCategoryCode] [varchar](3) NOT NULL,
	[PGSC_SectorCategoryName] [varchar](50) NULL,
	[PGSC_CreatedBy] [int] NULL,
	[PGSC_CreatedOn] [datetime] NULL,
	[PGSC_ModifiedBy] [int] NULL,
	[PGSC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpSector] PRIMARY KEY CLUSTERED 
(
	[PGSC_SectorCategoryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


