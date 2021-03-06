/****** Object:  View [dbo].[ViewMutualFundNP]    Script Date: 06/24/2009 14:45:29 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewMutualFundNP]'))
DROP VIEW [dbo].[ViewMutualFundNP]
GO
/****** Object:  View [dbo].[ViewMutualFundNP]    Script Date: 06/24/2009 14:45:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewMutualFundNP]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.ViewMutualFundNP
AS
SELECT     dbo.Adviser.A_AdviserId, dbo.AdviserRM.AR_RMId, dbo.Customer.C_CustomerId, dbo.CustomerPortfolio.CP_PortfolioId, 
                      dbo.CustomerMutualFundNetPosition.CMFNP_MFNPId, dbo.CustomerMutualFundNetPosition.CMFA_AccountId, 
                      dbo.CustomerMutualFundNetPosition.PASP_SchemePlanCode, dbo.CustomerMutualFundNetPosition.CMFNP_NetHoldings, 
                      dbo.CustomerMutualFundNetPosition.CMFNP_MarketPrice, dbo.CustomerMutualFundNetPosition.CMFNP_ValuationDate, 
                      dbo.CustomerMutualFundNetPosition.CMFNP_SalesQuantity, dbo.CustomerMutualFundNetPosition.CMFNP_RealizedSaleProceeds, 
                      dbo.CustomerMutualFundNetPosition.CMFNP_AveragePrice, dbo.CustomerMutualFundNetPosition.[CMFNP_RealizedP/L], 
                      dbo.CustomerMutualFundNetPosition.CMFNP_CostOfSales, dbo.CustomerMutualFundNetPosition.CMFNP_NetCost, 
                      dbo.CustomerMutualFundNetPosition.CMFNP_CurrentValue AS Expr2, dbo.CustomerMutualFundNetPosition.CMFNP_CreatedBy, 
                      dbo.CustomerMutualFundNetPosition.CMFNP_CreatedOn, dbo.CustomerMutualFundNetPosition.CMFNP_ModifiedOn, 
                      dbo.CustomerMutualFundNetPosition.CMFNP_ModifiedBy, dbo.ProductAMCSchemePlan.PASP_SchemePlanName, dbo.ProductAssetGroup.PAG_AssetGroupName, 
                      dbo.ProductAssetGroup.PAG_AssetGroupCode, dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode, 
                      dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryName, dbo.ProductAssetInstrumentSubCategory.PAISC_AssetInstrumentSubCategoryCode, 
                      dbo.ProductAssetInstrumentSubCategory.PAISC_AssetInstrumentSubCategoryName, ProductAMCSchemePlan_1.PASP_SchemePlanName AS Expr1, 
                      dbo.CustomerMutualFundNetPosition.CMFNP_CurrentValue, dbo.CustomerMutualFundNetPosition.CMFNP_DividendIncome, 
                      dbo.ProductAssetInstrumentSubSubCategory.PAISSC_AssetInstrumentSubSubCategoryCode, 
                      dbo.ProductAssetInstrumentSubSubCategory.PAG_AssetGroupCode AS Expr3
FROM         dbo.Adviser INNER JOIN
                      dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId INNER JOIN
                      dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId INNER JOIN
                      dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId INNER JOIN
                      dbo.CustomerMutualFundAccount ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerMutualFundAccount.CP_PortfolioId INNER JOIN
                      dbo.CustomerMutualFundNetPosition ON dbo.CustomerMutualFundAccount.CMFA_AccountId = dbo.CustomerMutualFundNetPosition.CMFA_AccountId INNER JOIN
                      dbo.ProductAMCSchemePlan ON dbo.CustomerMutualFundNetPosition.PASP_SchemePlanCode = dbo.ProductAMCSchemePlan.PASP_SchemePlanCode INNER JOIN
                      dbo.ProductAssetGroup ON dbo.CustomerMutualFundAccount.PAG_AssetGroupCode = dbo.ProductAssetGroup.PAG_AssetGroupCode INNER JOIN
                      dbo.ProductAssetInstrumentCategory ON dbo.ProductAssetGroup.PAG_AssetGroupCode = dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode INNER JOIN
                      dbo.ProductAssetInstrumentSubCategory ON 
                      dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentSubCategory.PAIC_AssetInstrumentCategoryCode AND 
                      dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode = dbo.ProductAssetInstrumentSubCategory.PAG_AssetGroupCode INNER JOIN
                      dbo.ProductAMCSchemePlan AS ProductAMCSchemePlan_1 ON 
                      dbo.CustomerMutualFundNetPosition.PASP_SchemePlanCode = ProductAMCSchemePlan_1.PASP_SchemePlanCode INNER JOIN
                      dbo.ProductAssetInstrumentSubSubCategory ON 
                      dbo.ProductAMCSchemePlan.PAISSC_AssetInstrumentSubSubCategoryCode = dbo.ProductAssetInstrumentSubSubCategory.PAISSC_AssetInstrumentSubSubCategoryCode
                       AND 
                      dbo.ProductAMCSchemePlan.PAISC_AssetInstrumentSubCategoryCode = dbo.ProductAssetInstrumentSubSubCategory.PAISC_AssetInstrumentSubCategoryCode AND 
                      dbo.ProductAMCSchemePlan.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentSubSubCategory.PAIC_AssetInstrumentCategoryCode AND 
                      dbo.ProductAMCSchemePlan.PAG_AssetGroupCode = dbo.ProductAssetInstrumentSubSubCategory.PAG_AssetGroupCode AND 
                      dbo.ProductAssetInstrumentSubCategory.PAISC_AssetInstrumentSubCategoryCode = dbo.ProductAssetInstrumentSubSubCategory.PAISC_AssetInstrumentSubCategoryCode
                       AND 
                      dbo.ProductAssetInstrumentSubCategory.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentSubSubCategory.PAIC_AssetInstrumentCategoryCode AND 
                      dbo.ProductAssetInstrumentSubCategory.PAG_AssetGroupCode = dbo.ProductAssetInstrumentSubSubCategory.PAG_AssetGroupCode AND 
                      ProductAMCSchemePlan_1.PAISSC_AssetInstrumentSubSubCategoryCode = dbo.ProductAssetInstrumentSubSubCategory.PAISSC_AssetInstrumentSubSubCategoryCode
                       AND 
                      ProductAMCSchemePlan_1.PAISC_AssetInstrumentSubCategoryCode = dbo.ProductAssetInstrumentSubSubCategory.PAISC_AssetInstrumentSubCategoryCode AND 
                      ProductAMCSchemePlan_1.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentSubSubCategory.PAIC_AssetInstrumentCategoryCode AND 
                      ProductAMCSchemePlan_1.PAG_AssetGroupCode = dbo.ProductAssetInstrumentSubSubCategory.PAG_AssetGroupCode
' 
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'ViewMutualFundNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[54] 4[18] 2[15] 3) )"
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
         Top = -361
         Left = -23
      End
      Begin Tables = 
         Begin Table = "Adviser"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 121
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdviserRM"
            Begin Extent = 
               Top = 6
               Left = 298
               Bottom = 121
               Right = 505
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Customer"
            Begin Extent = 
               Top = 6
               Left = 543
               Bottom = 121
               Right = 768
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerPortfolio"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 241
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerMutualFundAccount"
            Begin Extent = 
               Top = 126
               Left = 510
               Bottom = 241
               Right = 767
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "CustomerMutualFundNetPosition"
            Begin Extent = 
               Top = 126
               Left = 247
               Bottom = 241
               Right = 472
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAMCSchemePlan"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 361
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewMutualFundNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'ViewMutualFundNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'        Right = 347
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "ProductAssetGroup"
            Begin Extent = 
               Top = 246
               Left = 385
               Bottom = 361
               Right = 575
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetInstrumentCategory"
            Begin Extent = 
               Top = 366
               Left = 38
               Bottom = 481
               Right = 301
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetInstrumentSubCategory"
            Begin Extent = 
               Top = 366
               Left = 339
               Bottom = 481
               Right = 626
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ProductAMCSchemePlan_1"
            Begin Extent = 
               Top = 508
               Left = 376
               Bottom = 616
               Right = 684
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetInstrumentSubSubCategory"
            Begin Extent = 
               Top = 514
               Left = 29
               Bottom = 631
               Right = 348
            End
            DisplayFlags = 280
            TopColumn = 0
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
         Column = 4665
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewMutualFundNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'ViewMutualFundNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewMutualFundNP'
GO
