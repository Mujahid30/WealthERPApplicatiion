/****** Object:  Table [dbo].[WerpKarvyCustomerTypeDataTranslatorMapping]    Script Date: 06/23/2009 19:01:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpKarvyCustomerTypeDataTranslatorMapping]') AND type in (N'U'))
DROP TABLE [dbo].[WerpKarvyCustomerTypeDataTranslatorMapping]
GO
/****** Object:  Table [dbo].[WerpKarvyCustomerTypeDataTranslatorMapping]    Script Date: 06/23/2009 19:01:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpKarvyCustomerTypeDataTranslatorMapping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WerpKarvyCustomerTypeDataTranslatorMapping](
	[WKCTDTM_TaxStatus] [varchar](5) NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[XCST_CustomerSubTypeCode] [varchar](5) NULL,
	[WKCTDTM_CreatedBy] [int] NULL,
	[WKCTDTM_CreatedOn] [datetime] NULL,
	[WKCTDTM_ModifiedBy] [int] NULL,
	[WKCTDTM_ModifiedOn] [datetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
