
GO

/****** Object:  Table [dbo].[CustomerEquityOdinNSEXtrnlTransactionInput]    Script Date: 06/11/2009 12:05:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityOdinNSEXtrnlTransactionInput](
	[CEONXTI_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEONXTI_TradeNum] [varchar](50) NULL,
	[CEONXTI_AssetCode] [varchar](50) NULL,
	[CEONXTI_ScripCode] [varchar](50) NULL,
	[CEONXTI_AssetIdentifier] [varchar](50) NULL,
	[CEONXTI_ScripName] [varchar](100) NULL,
	[CEONXTI_Field6] [varchar](50) NULL,
	[CEONXTI_Field7] [varchar](50) NULL,
	[CEONXTI_Field8] [varchar](50) NULL,
	[CEONXTI_Field9] [varchar](50) NULL,
	[CEONXTI_Field10] [varchar](50) NULL,
	[CEONXTI_BuySell] [varchar](50) NULL,
	[CEONXTI_Quantity] [varchar](50) NULL,
	[CEONXTI_Rate] [varchar](50) NULL,
	[CEONXTI_Field14] [varchar](50) NULL,
	[CEONXTI_TradeAccountNum] [varchar](50) NULL,
	[CEONXTI_TerminalId] [varchar](50) NULL,
	[CEONXTI_Field17] [varchar](50) NULL,
	[CEONXTI_Field18] [varchar](50) NULL,
	[CEONXTI_Field19] [varchar](50) NULL,
	[CEONXTI_TradeDate] [varchar](50) NULL,
	[CEONXTI_Field21] [varchar](50) NULL,
	[CEONXTI_Field22] [varchar](50) NULL,
	[CEONXTI_Field23] [varchar](50) NULL,
	[CEONXTI_Field24] [varchar](50) NULL,
	[CEONXTI_CreatedBy] [int] NULL,
	[CEONXTI_CreatedOn] [datetime] NULL,
	[CEONXTI_ModifiedOn] [datetime] NULL,
	[CEONXTI_ModifiedBy] [int] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerEquityOdinXtrnlTransactionInput] PRIMARY KEY CLUSTERED 
(
	[CEONXTI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


