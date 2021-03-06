/****** Object:  Table [dbo].[XMLBroker]    Script Date: 06/23/2009 19:02:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLBroker]') AND type in (N'U'))
DROP TABLE [dbo].[XMLBroker]
GO
/****** Object:  Table [dbo].[XMLBroker]    Script Date: 06/23/2009 19:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLBroker]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLBroker](
	[XB_BrokerCode] [varchar](5) NOT NULL,
	[XB_BrokerName] [varchar](70) NULL,
	[XB_BrokerIdentifier] [varchar](30) NULL,
	[XB_CreatedBy] [int] NULL,
	[XB_CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLBroker] PRIMARY KEY CLUSTERED 
(
	[XB_BrokerCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
