/****** Object:  View [dbo].[ViewCustomerSystematicSetupSchemes]    Script Date: 06/24/2009 14:45:15 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewCustomerSystematicSetupSchemes]'))
DROP VIEW [dbo].[ViewCustomerSystematicSetupSchemes]
GO
/****** Object:  View [dbo].[ViewCustomerSystematicSetupSchemes]    Script Date: 06/24/2009 14:45:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewCustomerSystematicSetupSchemes]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.ViewCustomerSystematicSetupSchemes
AS
SELECT     dbo.Customer.C_CustomerId, dbo.Customer.AR_RMId, dbo.CustomerPortfolio.CP_PortfolioId, dbo.CustomerMutualFundAccount.CMFA_AccountId, 
                      dbo.CustomerMutualFundAccount.PAG_AssetGroupCode, dbo.CustomerMutualFundSystematicSetup.CMFSS_SystematicSetupId, 
                      dbo.CustomerMutualFundSystematicSetup.PASP_SchemePlanCode, dbo.CustomerMutualFundSystematicSetup.XSTT_SystematicTypeCode, 
                      dbo.CustomerMutualFundSystematicSetup.CMFSS_StartDate, dbo.CustomerMutualFundSystematicSetup.CMFSS_EndDate, 
                      dbo.CustomerMutualFundSystematicSetup.CMFSS_SystematicDate, dbo.CustomerMutualFundSystematicSetup.XF_FrequencyCode, 
                      dbo.CustomerMutualFundSystematicSetup.XES_SourceCode, dbo.CustomerMutualFundSystematicSetup.XPM_PaymentModeCode, 
                      dbo.CustomerMutualFundSystematicSetup.CMFSS_CreatedBy, dbo.CustomerMutualFundSystematicSetup.CMFSS_ModifiedBy
FROM         dbo.Customer INNER JOIN
                      dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId INNER JOIN
                      dbo.CustomerMutualFundAccount ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerMutualFundAccount.CP_PortfolioId INNER JOIN
                      dbo.CustomerMutualFundSystematicSetup ON dbo.CustomerMutualFundAccount.CMFA_AccountId = dbo.CustomerMutualFundSystematicSetup.CMFA_AccountId
' 
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'ViewCustomerSystematicSetupSchemes', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Customer"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 123
               Right = 271
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerPortfolio"
            Begin Extent = 
               Top = 6
               Left = 309
               Bottom = 123
               Right = 488
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerMutualFundAccount"
            Begin Extent = 
               Top = 6
               Left = 526
               Bottom = 123
               Right = 751
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerMutualFundSystematicSetup"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 243
               Right = 271
            End
            DisplayFlags = 280
            TopColumn = 12
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewCustomerSystematicSetupSchemes'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'ViewCustomerSystematicSetupSchemes', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewCustomerSystematicSetupSchemes'
GO
