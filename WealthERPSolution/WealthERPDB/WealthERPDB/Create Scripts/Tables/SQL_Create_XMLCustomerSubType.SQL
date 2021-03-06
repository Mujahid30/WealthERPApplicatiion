/****** Object:  Table [dbo].[XMLCustomerSubType]    Script Date: 06/23/2009 19:02:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLCustomerSubType]') AND type in (N'U'))
DROP TABLE [dbo].[XMLCustomerSubType]
GO
/****** Object:  Table [dbo].[XMLCustomerSubType]    Script Date: 06/23/2009 19:02:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLCustomerSubType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLCustomerSubType](
	[XCST_CustomerSubTypeCode] [varchar](5) NOT NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[XCST_CustomersubTypeName] [varchar](30) NULL,
	[XCST_CreatedBy] [int] NULL,
	[XCST_CreatedOn] [datetime] NULL,
	[XCST_ModifiedBy] [int] NULL,
	[XCST_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLCustomerSubType] PRIMARY KEY CLUSTERED 
(
	[XCST_CustomerSubTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_XMLCustomerSubType_XMLCustomerType]') AND parent_object_id = OBJECT_ID(N'[dbo].[XMLCustomerSubType]'))
ALTER TABLE [dbo].[XMLCustomerSubType]  WITH CHECK ADD  CONSTRAINT [FK_XMLCustomerSubType_XMLCustomerType] FOREIGN KEY([XCT_CustomerTypeCode])
REFERENCES [dbo].[XMLCustomerType] ([XCT_CustomerTypeCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_XMLCustomerSubType_XMLCustomerType]') AND parent_object_id = OBJECT_ID(N'[dbo].[XMLCustomerSubType]'))
ALTER TABLE [dbo].[XMLCustomerSubType] CHECK CONSTRAINT [FK_XMLCustomerSubType_XMLCustomerType]
GO
