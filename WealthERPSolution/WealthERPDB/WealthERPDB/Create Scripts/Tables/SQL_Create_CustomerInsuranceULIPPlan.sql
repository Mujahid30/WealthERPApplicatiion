
GO

/****** Object:  Table [dbo].[CustomerInsuranceULIPPlan]    Script Date: 06/11/2009 13:08:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerInsuranceULIPPlan](
	[CIUP_ULIPPlanId] [int] IDENTITY(1000,1) NOT NULL,
	[CINP_InsuranceNPId] [int] NULL,
	[WUSP_ULIPSubPlanCode] [int] NULL,
	[CIUP_AllocationPer] [numeric](5, 2) NULL,
	[CIUP_Unit] [numeric](10, 3) NULL,
	[CIUP_PurchasePrice] [numeric](18, 3) NULL,
	[CIUP_PurchaseDate] [datetime] NULL,
	[CIUP_CreatedBy] [int] NULL,
	[CIUP_CreatedOn] [datetime] NULL,
	[CIUP_ModifiedBy] [int] NULL,
	[CIUP_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerInsurabceULIPPlan] PRIMARY KEY CLUSTERED 
(
	[CIUP_ULIPPlanId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CustomerInsuranceULIPPlan]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsurabceULIPPlan_CustomerInsurancePortfolio] FOREIGN KEY([CINP_InsuranceNPId])
REFERENCES [dbo].[CustomerInsuranceNetPosition] ([CINP_InsuranceNPId])
GO

ALTER TABLE [dbo].[CustomerInsuranceULIPPlan] CHECK CONSTRAINT [FK_CustomerInsurabceULIPPlan_CustomerInsurancePortfolio]
GO

ALTER TABLE [dbo].[CustomerInsuranceULIPPlan]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsurabceULIPPlan_WerpULIPSubPlan] FOREIGN KEY([WUSP_ULIPSubPlanCode])
REFERENCES [dbo].[WerpULIPSubPlan] ([WUSP_ULIPSubPlanCode])
GO

ALTER TABLE [dbo].[CustomerInsuranceULIPPlan] CHECK CONSTRAINT [FK_CustomerInsurabceULIPPlan_WerpULIPSubPlan]
GO


