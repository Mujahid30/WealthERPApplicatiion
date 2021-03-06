/****** Object:  Table [dbo].[WerpCustomerTypeDataTranslatorMapping]    Script Date: 06/23/2009 19:01:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpCustomerTypeDataTranslatorMapping]') AND type in (N'U'))
DROP TABLE [dbo].[WerpCustomerTypeDataTranslatorMapping]
GO
/****** Object:  Table [dbo].[WerpCustomerTypeDataTranslatorMapping]    Script Date: 06/23/2009 19:01:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpCustomerTypeDataTranslatorMapping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WerpCustomerTypeDataTranslatorMapping](
	[WKCTDTM_TaxStaus] [varchar](10) NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[XCST_CustomerSubTypeCode] [varchar](5) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerSubType]') AND parent_object_id = OBJECT_ID(N'[dbo].[WerpCustomerTypeDataTranslatorMapping]'))
ALTER TABLE [dbo].[WerpCustomerTypeDataTranslatorMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerSubType] FOREIGN KEY([XCST_CustomerSubTypeCode])
REFERENCES [dbo].[XMLCustomerSubType] ([XCST_CustomerSubTypeCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerSubType]') AND parent_object_id = OBJECT_ID(N'[dbo].[WerpCustomerTypeDataTranslatorMapping]'))
ALTER TABLE [dbo].[WerpCustomerTypeDataTranslatorMapping] CHECK CONSTRAINT [FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerSubType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerType]') AND parent_object_id = OBJECT_ID(N'[dbo].[WerpCustomerTypeDataTranslatorMapping]'))
ALTER TABLE [dbo].[WerpCustomerTypeDataTranslatorMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerType] FOREIGN KEY([XCT_CustomerTypeCode])
REFERENCES [dbo].[XMLCustomerType] ([XCT_CustomerTypeCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerType]') AND parent_object_id = OBJECT_ID(N'[dbo].[WerpCustomerTypeDataTranslatorMapping]'))
ALTER TABLE [dbo].[WerpCustomerTypeDataTranslatorMapping] CHECK CONSTRAINT [FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerType]
GO
