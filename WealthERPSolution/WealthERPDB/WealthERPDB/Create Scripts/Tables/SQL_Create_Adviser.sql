
GO

/****** Object:  Table [dbo].[Adviser]    Script Date: 06/11/2009 10:42:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Adviser](
	[A_AdviserId] [int] IDENTITY(1000,1) NOT NULL,
	[U_UserId] [int] NULL,
	[A_OrgName] [varchar](25) NOT NULL,
	[A_AddressLine1] [varchar](25) NOT NULL,
	[A_AddressLine2] [varchar](25) NULL,
	[A_AddressLine3] [varchar](25) NULL,
	[A_City] [varchar](25) NOT NULL,
	[A_State] [varchar](25) NOT NULL,
	[A_PinCode] [numeric](6, 0) NOT NULL,
	[A_Country] [varchar](25) NOT NULL,
	[A_Phone1STD] [numeric](4, 0) NOT NULL,
	[A_Phone1ISD] [numeric](4, 0) NOT NULL,
	[A_Phone1Number] [numeric](8, 0) NOT NULL,
	[A_Phone2STD] [numeric](4, 0) NULL,
	[A_Phone2ISD] [numeric](4, 0) NULL,
	[A_Phone2Number] [numeric](8, 0) NULL,
	[A_Email] [varchar](max) NULL,
	[A_FAXISD] [numeric](4, 0) NULL,
	[A_FAXSTD] [numeric](4, 0) NULL,
	[A_FAX] [numeric](8, 0) NULL,
	[XABT_BusinessTypeCode] [varchar](5) NOT NULL,
	[A_ContactPersonFirstName] [varchar](25) NOT NULL,
	[A_ContactPersonMiddleName] [varchar](25) NULL,
	[A_ContactPersonLastName] [varchar](25) NULL,
	[A_ContactPersonMobile] [numeric](10, 0) NULL,
	[A_IsMultiBranch] [tinyint] NULL,
	[A_AdviserLogo] [varchar](50) NULL,
	[A_CreatedBy] [int] NOT NULL,
	[A_CreatedOn] [datetime] NOT NULL,
	[A_ModifiedBy] [int] NOT NULL,
	[A_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_AdvisorMaster] PRIMARY KEY CLUSTERED 
(
	[A_AdviserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Advisor Master Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Adviser'
GO

ALTER TABLE [dbo].[Adviser]  WITH CHECK ADD  CONSTRAINT [FK_Adviser_User] FOREIGN KEY([U_UserId])
REFERENCES [dbo].[User] ([U_UserId])
GO

ALTER TABLE [dbo].[Adviser] CHECK CONSTRAINT [FK_Adviser_User]
GO

ALTER TABLE [dbo].[Adviser]  WITH CHECK ADD  CONSTRAINT [FK_Adviser_XMLAdviserBusinessType] FOREIGN KEY([XABT_BusinessTypeCode])
REFERENCES [dbo].[XMLAdviserBusinessType] ([XABT_BusinessTypeCode])
GO

ALTER TABLE [dbo].[Adviser] CHECK CONSTRAINT [FK_Adviser_XMLAdviserBusinessType]
GO


