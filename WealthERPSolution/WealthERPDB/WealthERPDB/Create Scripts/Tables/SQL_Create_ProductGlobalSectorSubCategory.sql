USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductGlobalSectorSubCategory]    Script Date: 06/12/2009 18:20:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductGlobalSectorSubCategory](
	[PGSSC_SectorSubCategoryCode] [varchar](6) NOT NULL,
	[PGSC_SectorCategoryCode] [varchar](3) NOT NULL,
	[PGSSC_SectorSubCategoryName] [varchar](50) NULL,
	[PGSSC_CreatedBy] [int] NULL,
	[PGSSC_CreatedOn] [datetime] NULL,
	[PGSSC_ModifiedBy] [int] NULL,
	[PGSSC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductGlobalSectorSubCategory] PRIMARY KEY CLUSTERED 
(
	[PGSSC_SectorSubCategoryCode] ASC,
	[PGSC_SectorCategoryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ProductGlobalSectorSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductGlobalSectorSubCategory_ProductGlobalSectorCategory] FOREIGN KEY([PGSC_SectorCategoryCode])
REFERENCES [dbo].[ProductGlobalSectorCategory] ([PGSC_SectorCategoryCode])
GO

ALTER TABLE [dbo].[ProductGlobalSectorSubCategory] CHECK CONSTRAINT [FK_ProductGlobalSectorSubCategory_ProductGlobalSectorCategory]
GO


