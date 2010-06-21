
GO

/****** Object:  Table [dbo].[CustomerMFCAMSXtrnlTransaction]    Script Date: 06/11/2009 15:36:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFCAMSXtrnlTransaction](
	[CIMFCXT_UploadId] [bigint] IDENTITY(1000,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[CMFT_MFTransId] [int] NULL,
	[CIMFCXT_AMCCode] [char](3) NULL,
	[CIMFCXT_FolioNum] [varchar](12) NULL,
	[CIMFCXT_ProductCode] [varchar](7) NULL,
	[CIMFCXT_Scheme] [varchar](100) NULL,
	[CIMFCXT_InvestorName] [varchar](60) NULL,
	[CIMFCXT_TransactionType] [varchar](7) NULL,
	[CIMFCXT_TransactionNum] [varchar](16) NULL,
	[CIMFCXT_TransactionMode] [char](1) NULL,
	[CIMFCXT_TransactionStatus] [varchar](50) NULL,
	[CIMFCXT_UserCode] [varchar](25) NULL,
	[CIMFCXT_UserTransactionNum] [varchar](50) NULL,
	[CIMFCXT_ValueDate] [datetime] NULL,
	[CIMFCXT_PostDate] [datetime] NULL,
	[CIMFCXT_Price] [numeric](10, 5) NULL,
	[CIMFCXT_Units] [numeric](12, 6) NULL,
	[CIMFCXT_Amount] [numeric](12, 6) NULL,
	[CIMFCXT_BrokerCode] [varchar](25) NULL,
	[CIMFCXT_SubBrokerCode] [varchar](25) NULL,
	[CIMFCXT_BrokeragePercentage] [numeric](3, 0) NULL,
	[CIMFCXT_BrokerageAmount] [numeric](10, 5) NULL,
	[CIMFCXT_Dummy1] [varchar](50) NULL,
	[CIMFCXT_FeedDate] [datetime] NULL,
	[CIMFCXT_Dummy2] [varchar](50) NULL,
	[CIMFCXT_Dummy3] [varchar](50) NULL,
	[CIMFCXT_ApplicationNum] [varchar](25) NULL,
	[CIMFCXT_TransactionNature] [varchar](25) NULL,
	[CIMFCXT_TaxStatus] [varchar](25) NULL,
	[CIMFCXT_AlternateBroker] [varchar](50) NULL,
	[CIMFCXT_AlternateFolio] [varchar](16) NULL,
	[CIMFCXT_ReinvestmentFlag] [char](1) NULL,
	[CIMFCXT_OldFolio] [varchar](16) NULL,
	[CIMFCXT_SequenceNum] [varchar](16) NULL,
	[CIMFCXT_MultipleBroker] [varchar](16) NULL,
	[CIMFCXT_Tax] [numeric](12, 6) NULL,
	[CIMFCXT_STT] [numeric](12, 6) NULL,
	[CIMFCXT_SchemeType] [varchar](50) NULL,
	[CIMFCXT_EntryLoad] [numeric](12, 6) NULL,
	[CIMFCXT_ScanRefNum] [varchar](50) NULL,
	[CIMFCXT_InvestorIIN] [varchar](50) NULL,
	[CIMFCXT_CreatedBy] [int] NULL,
	[CIMFCXT_CreatedOn] [datetime] NULL,
	[CIMFCXT_ModifiedBy] [int] NULL,
	[CIMFCXT_ModifiedOn] [datetime] NULL,
	[CIMFCXT_TaxStatus1] [varchar](25) NULL,
 CONSTRAINT [PK_CAMSTransactionUpload] PRIMARY KEY CLUSTERED 
(
	[CIMFCXT_UploadId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer CAMS MF Transaction Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMFCAMSXtrnlTransaction'
GO

ALTER TABLE [dbo].[CustomerMFCAMSXtrnlTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCAMSMFTransaction_CustomerMFTransaction] FOREIGN KEY([CMFT_MFTransId])
REFERENCES [dbo].[CustomerMutualFundTransaction] ([CMFT_MFTransId])
GO

ALTER TABLE [dbo].[CustomerMFCAMSXtrnlTransaction] CHECK CONSTRAINT [FK_CustomerCAMSMFTransaction_CustomerMFTransaction]
GO


