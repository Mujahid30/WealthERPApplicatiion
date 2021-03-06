/****** Object:  Table [dbo].[XMLInsuranceIssuer]    Script Date: 06/23/2009 19:03:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLInsuranceIssuer]') AND type in (N'U'))
DROP TABLE [dbo].[XMLInsuranceIssuer]
GO
/****** Object:  Table [dbo].[XMLInsuranceIssuer]    Script Date: 06/23/2009 19:03:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLInsuranceIssuer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLInsuranceIssuer](
	[XII_InsuranceIssuerCode] [varchar](5) NOT NULL,
	[XII_InsuranceIssuerName] [varchar](50) NULL,
	[XII_CreatedBy] [int] NULL,
	[XII_CreatedOn] [datetime] NULL,
	[XII_ModifiedBy] [int] NULL,
	[XII_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpInsuranceIssuer_XML] PRIMARY KEY CLUSTERED 
(
	[XII_InsuranceIssuerCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
