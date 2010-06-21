
GO

/****** Object:  Table [dbo].[WerpRejectReason]    Script Date: 06/12/2009 18:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WerpRejectReason](
	[WRR_RejectReasonCode] [int] IDENTITY(1,1) NOT NULL,
	[WRR_RejectReasonDescription] [varchar](100) NULL,
	[WRR_CreatedBy] [int] NULL,
	[WRR_CreatedOn] [datetime] NULL,
	[WRR_ModifiedBy] [int] NULL,
	[WRR_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpRejectReason] PRIMARY KEY CLUSTERED 
(
	[WRR_RejectReasonCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


