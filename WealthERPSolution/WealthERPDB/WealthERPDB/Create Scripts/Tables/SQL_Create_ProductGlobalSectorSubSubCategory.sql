USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductGlobalSectorSubSubCategory]    Script Date: 06/12/2009 18:21:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductGlobalSectorSubSubCategory](
	[PGSSSC_SectorSubSubCategoryCode] [varchar](9) NOT NULL,
	[PGSC_SectorCategoryCode] [varchar](3) NOT NULL,
	[PGSSC_SectorSubCategoryCode] [varchar](6) NOT NULL,
	[PGSSSC_ExternalSectorCode] [varchar](50) NULL,
	[PGSSSC_SectorSubSubCategoryName] [varchar](100) NULL,
	[PGSSSC_CreatedBy] [int] NULL,
	[PGSSSC_CreatedOn] [datetime] NULL,
	[PGSSSC_ModifiedBy] [int] NULL,
	[PGSSSC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductGlobalSectorSubSubCategory_1] PRIMARY KEY CLUSTERED 
(
	[PGSSSC_SectorSubSubCategoryCode] ASC,
	[PGSC_SectorCategoryCode] ASC,
	[PGSSC_SectorSubCategoryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ProductGlobalSectorSubSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductGlobalSectorSubSubCategory_ProductGlobalSectorSubCategory] FOREIGN KEY([PGSSC_SectorSubCategoryCode], [PGSC_SectorCategoryCode])
REFERENCES [dbo].[ProductGlobalSectorSubCategory] ([PGSSC_SectorSubCategoryCode], [PGSC_SectorCategoryCode])
GO

ALTER TABLE [dbo].[ProductGlobalSectorSubSubCategory] CHECK CONSTRAINT [FK_ProductGlobalSectorSubSubCategory_ProductGlobalSectorSubCategory]
GO


