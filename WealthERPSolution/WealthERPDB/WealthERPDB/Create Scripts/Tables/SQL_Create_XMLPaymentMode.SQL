/****** Object:  Table [dbo].[XMLPaymentMode]    Script Date: 06/23/2009 19:03:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLPaymentMode]') AND type in (N'U'))
DROP TABLE [dbo].[XMLPaymentMode]
GO
/****** Object:  Table [dbo].[XMLPaymentMode]    Script Date: 06/23/2009 19:03:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLPaymentMode]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLPaymentMode](
	[XPM_PaymentModeCode] [varchar](5) NOT NULL,
	[XPM_PaymentMode] [varchar](20) NULL,
	[XPM_CreatedBy] [int] NULL,
	[XPM_CreatedOn] [datetime] NULL,
	[XPM_ModifiedBy] [int] NULL,
	[XPM_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLPaymentMode] PRIMARY KEY CLUSTERED 
(
	[XPM_PaymentModeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
