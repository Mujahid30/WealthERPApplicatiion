
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 06/11/2009 11:50:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Customer](
	[C_CustomerId] [int] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[AR_RMId] [int] NOT NULL,
	[U_UMId] [int] NOT NULL,
	[C_CustCode] [varchar](10) NULL,
	[C_ProfilingDate] [datetime] NULL,
	[C_FirstName] [varchar](25) NULL,
	[C_MiddleName] [varchar](25) NULL,
	[C_LastName] [varchar](75) NULL,
	[C_Gender] [varchar](10) NULL,
	[C_DOB] [datetime] NULL,
	[XCT_CustomerTypeCode] [varchar](5) NULL,
	[XCST_CustomerSubTypeCode] [varchar](5) NULL,
	[C_Salutation] [varchar](5) NULL,
	[C_PANNum] [varchar](10) NULL,
	[C_Adr1Line1] [varchar](75) NULL,
	[C_Adr1Line2] [varchar](75) NULL,
	[C_Adr1Line3] [varchar](75) NULL,
	[C_Adr1PinCode] [numeric](10, 0) NULL,
	[C_Adr1City] [varchar](25) NULL,
	[C_Adr1State] [varchar](25) NULL,
	[C_Adr1Country] [varchar](25) NULL,
	[C_Adr2Line1] [varchar](75) NULL,
	[C_Adr2Line2] [varchar](75) NULL,
	[C_Adr2Line3] [varchar](75) NULL,
	[C_Adr2PinCode] [numeric](10, 0) NULL,
	[C_Adr2City] [varchar](20) NULL,
	[C_Adr2State] [varchar](20) NULL,
	[C_Adr2Country] [varchar](20) NULL,
	[C_ResISDCode] [numeric](4, 0) NULL,
	[C_ResSTDCode] [numeric](5, 0) NULL,
	[C_ResPhoneNum] [numeric](10, 0) NULL,
	[C_OfcISDCode] [numeric](4, 0) NULL,
	[C_OfcSTDCode] [numeric](5, 0) NULL,
	[C_OfcPhoneNum] [numeric](10, 0) NULL,
	[C_Email] [varchar](75) NOT NULL,
	[C_AltEmail] [varchar](75) NULL,
	[C_Mobile1] [numeric](14, 0) NULL,
	[C_Mobile2] [numeric](14, 0) NULL,
	[C_ISDFax] [numeric](4, 0) NULL,
	[C_STDFax] [numeric](5, 0) NULL,
	[C_Fax] [numeric](25, 0) NULL,
	[C_OfcFax] [numeric](25, 0) NULL,
	[C_OfcFaxISD] [numeric](4, 0) NULL,
	[C_OfcFaxSTD] [numeric](5, 0) NULL,
	[XO_OccupationCode] [varchar](5) NULL,
	[XQ_QualificationCode] [varchar](5) NULL,
	[C_MarriageDate] [datetime] NULL,
	[XMS_MaritalStatusCode] [varchar](5) NULL,
	[XN_NationalityCode] [varchar](5) NULL,
	[C_RBIRefNum] [varchar](25) NULL,
	[C_RBIApprovalDate] [datetime] NULL,
	[C_CompanyName] [varchar](50) NULL,
	[C_OfcAdrLine1] [varchar](75) NULL,
	[C_OfcAdrLine2] [varchar](75) NULL,
	[C_OfcAdrLine3] [varchar](75) NULL,
	[C_OfcAdrPinCode] [numeric](10, 0) NULL,
	[C_OfcAdrCity] [varchar](25) NULL,
	[C_OfcAdrState] [varchar](25) NULL,
	[C_OfcAdrCountry] [varchar](25) NULL,
	[C_RegistrationDate] [datetime] NULL,
	[C_CommencementDate] [datetime] NULL,
	[C_RegistrationPlace] [varchar](20) NULL,
	[C_RegistrationNum] [varchar](25) NULL,
	[C_CompanyWebsite] [varchar](25) NULL,
	[C_CreatedOn] [datetime] NOT NULL,
	[C_CreatedBy] [int] NOT NULL,
	[C_ModifiedOn] [datetime] NOT NULL,
	[C_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerMaster] PRIMARY KEY CLUSTERED 
(
	[C_CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Master' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer'
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLCustomerSubType] FOREIGN KEY([XCST_CustomerSubTypeCode])
REFERENCES [dbo].[XMLCustomerSubType] ([XCST_CustomerSubTypeCode])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLCustomerSubType]
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLCustomerType] FOREIGN KEY([XCT_CustomerTypeCode])
REFERENCES [dbo].[XMLCustomerType] ([XCT_CustomerTypeCode])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLCustomerType]
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLMaritalStatus] FOREIGN KEY([XMS_MaritalStatusCode])
REFERENCES [dbo].[XMLMaritalStatus] ([XMS_MaritalStatusCode])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLMaritalStatus]
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLNationality] FOREIGN KEY([XN_NationalityCode])
REFERENCES [dbo].[XMLNationality] ([XN_NationalityCode])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLNationality]
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLOccupation] FOREIGN KEY([XO_OccupationCode])
REFERENCES [dbo].[XMLOccupation] ([XO_OccupationCode])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLOccupation]
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLQualification] FOREIGN KEY([XQ_QualificationCode])
REFERENCES [dbo].[XMLQualification] ([XQ_QualificationCode])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLQualification]
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMaster_RMMaster] FOREIGN KEY([AR_RMId])
REFERENCES [dbo].[AdviserRM] ([AR_RMId])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_CustomerMaster_RMMaster]
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMaster_UserMaster] FOREIGN KEY([U_UMId])
REFERENCES [dbo].[User] ([U_UserId])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_CustomerMaster_UserMaster]
GO


