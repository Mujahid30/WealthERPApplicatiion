
GO

/****** Object:  Table [dbo].[AdviserRM]    Script Date: 06/11/2009 10:49:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AdviserRM](
	[AR_RMId] [int] IDENTITY(1000,1) NOT NULL,
	[A_AdviserId] [int] NOT NULL,
	[U_UserId] [int] NULL,
	[AR_FirstName] [varchar](50) NULL,
	[AR_MiddleName] [varchar](50) NULL,
	[AR_LastName] [varchar](50) NULL,
	[AR_OfficePhoneDirectISD] [numeric](4, 0) NULL,
	[AR_OfficePhoneDirectSTD] [numeric](4, 0) NULL,
	[AR_OfficePhoneDirect] [numeric](8, 0) NULL,
	[AR_OfficePhoneExtISD] [numeric](4, 0) NULL,
	[AR_OfficePhoneExtSTD] [numeric](4, 0) NULL,
	[AR_OfficePhoneExt] [numeric](8, 0) NULL,
	[AR_ResPhoneISD] [numeric](4, 0) NULL,
	[AR_ResPhoneSTD] [numeric](4, 0) NULL,
	[AR_ResPhone] [numeric](8, 0) NULL,
	[AR_Mobile] [numeric](10, 0) NULL,
	[AR_FaxISD] [numeric](4, 0) NULL,
	[AR_FaxSTD] [numeric](4, 0) NULL,
	[AR_Fax] [numeric](8, 0) NULL,
	[AR_Email] [varchar](max) NULL,
	[AR_CreatedBy] [int] NOT NULL,
	[AR_CreatedOn] [datetime] NOT NULL,
	[AR_ModifiedBy] [int] NOT NULL,
	[AR_ModifiedOn] [datetime] NOT NULL,
	[AR_JobFunction] [varchar](30) NULL,
 CONSTRAINT [PK_RMMaster] PRIMARY KEY CLUSTERED 
(
	[AR_RMId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'RM Master Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserRM'
GO

ALTER TABLE [dbo].[AdviserRM]  WITH CHECK ADD  CONSTRAINT [FK_RMMaster_AdvisorMaster] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO

ALTER TABLE [dbo].[AdviserRM] CHECK CONSTRAINT [FK_RMMaster_AdvisorMaster]
GO

ALTER TABLE [dbo].[AdviserRM]  WITH CHECK ADD  CONSTRAINT [FK_RMMaster_UserMaster] FOREIGN KEY([U_UserId])
REFERENCES [dbo].[User] ([U_UserId])
GO

ALTER TABLE [dbo].[AdviserRM] CHECK CONSTRAINT [FK_RMMaster_UserMaster]
GO


