
GO

/****** Object:  Table [dbo].[WerpProfileFilterCategory]    Script Date: 06/12/2009 18:35:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WerpProfileFilterCategory](
	[WPFC_FilterCategoryCode] [varchar](10) NOT NULL,
	[WPFC_FilterCategoryName] [varchar](50) NULL,
	[WPFC_AssetClass] [varchar](50) NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[WPFC_KYFCompliantFlag] [tinyint] NULL,
	[WPFC_CreatedBy] [int] NULL,
	[WPFC_CreatedOn] [datetime] NULL,
	[WPFC_ModifiedBy] [int] NULL,
	[WPFC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ProfileFilterCategory_XML] PRIMARY KEY CLUSTERED 
(
	[WPFC_FilterCategoryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[WerpProfileFilterCategory]  WITH CHECK ADD  CONSTRAINT [FK_WerpProfileFilterCategory_XMLCustomerType] FOREIGN KEY([XCT_CustomerTypeCode])
REFERENCES [dbo].[XMLCustomerType] ([XCT_CustomerTypeCode])
GO

ALTER TABLE [dbo].[WerpProfileFilterCategory] CHECK CONSTRAINT [FK_WerpProfileFilterCategory_XMLCustomerType]
GO


