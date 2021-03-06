/****** Object:  Table [dbo].[XMLAdviserBusinessType]    Script Date: 06/23/2009 19:02:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLAdviserBusinessType]') AND type in (N'U'))
DROP TABLE [dbo].[XMLAdviserBusinessType]
GO
/****** Object:  Table [dbo].[XMLAdviserBusinessType]    Script Date: 06/23/2009 19:02:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLAdviserBusinessType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLAdviserBusinessType](
	[XABT_BusinessTypeCode] [varchar](5) NOT NULL,
	[XABT_BusinessType] [varchar](30) NULL,
	[XABT_CreatedBy] [int] NULL,
	[XABT_CreatedOn] [datetime] NULL,
	[XABT_ModifiedBy] [int] NULL,
	[XABT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_BusinessType] PRIMARY KEY CLUSTERED 
(
	[XABT_BusinessTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'XMLAdviserBusinessType', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'BusinessType_XML Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'XMLAdviserBusinessType'
GO
