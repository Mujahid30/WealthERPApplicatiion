/****** Object:  Table [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]    Script Date: 06/23/2009 19:00:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]') AND type in (N'U'))
DROP TABLE [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]
GO
/****** Object:  Table [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]    Script Date: 06/23/2009 19:00:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping](
	[WCCTDTM_TaxStatus] [varchar](75) NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[XCST_CustomerSubTypeCode] [varchar](5) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerSubType]') AND parent_object_id = OBJECT_ID(N'[dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]'))
ALTER TABLE [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerSubType] FOREIGN KEY([XCST_CustomerSubTypeCode])
REFERENCES [dbo].[XMLCustomerSubType] ([XCST_CustomerSubTypeCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerSubType]') AND parent_object_id = OBJECT_ID(N'[dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]'))
ALTER TABLE [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping] CHECK CONSTRAINT [FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerSubType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerType]') AND parent_object_id = OBJECT_ID(N'[dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]'))
ALTER TABLE [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerType] FOREIGN KEY([XCT_CustomerTypeCode])
REFERENCES [dbo].[XMLCustomerType] ([XCT_CustomerTypeCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerType]') AND parent_object_id = OBJECT_ID(N'[dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]'))
ALTER TABLE [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping] CHECK CONSTRAINT [FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerType]
GO
