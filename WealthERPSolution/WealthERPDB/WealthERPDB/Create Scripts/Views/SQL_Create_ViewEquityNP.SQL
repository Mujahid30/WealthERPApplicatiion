/****** Object:  View [dbo].[ViewEquityNP]    Script Date: 06/24/2009 14:45:17 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewEquityNP]'))
DROP VIEW [dbo].[ViewEquityNP]
GO
/****** Object:  View [dbo].[ViewEquityNP]    Script Date: 06/24/2009 14:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewEquityNP]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.ViewEquityNP
AS
SELECT     dbo.Adviser.A_AdviserId, dbo.AdviserRM.AR_RMId, dbo.Customer.C_CustomerId, dbo.CustomerPortfolio.CP_PortfolioId, 
                      dbo.ProductEquityMaster.PEM_CompanyName, dbo.CustomerEquityNetPosition.CENP_EquityNPId, dbo.CustomerEquityNetPosition.PEM_ScripCode, 
                      dbo.CustomerEquityNetPosition.CETA_AccountId, dbo.CustomerEquityNetPosition.CENP_ValuationDate, 
                      dbo.CustomerEquityNetPosition.CENP_NetHoldings, dbo.CustomerEquityNetPosition.CENP_AveragePrice, 
                      dbo.CustomerEquityNetPosition.CENP_MarketPrice, dbo.CustomerEquityNetPosition.[CENP_RealizedP/L], 
                      dbo.CustomerEquityNetPosition.CENP_CostOfSales, dbo.CustomerEquityNetPosition.CENP_NetCost, 
                      dbo.CustomerEquityNetPosition.CENP_SpeculativeSaleQuantity, dbo.CustomerEquityNetPosition.CENP_DeliverySaleQuantity, 
                      dbo.CustomerEquityNetPosition.CENP_SaleQuantity, dbo.CustomerEquityNetPosition.[CENP_RealizedP/LForSpeculative], 
                      dbo.CustomerEquityNetPosition.[CENP_RealizedP/LForDelivery], dbo.CustomerEquityNetPosition.CENP_CostOfSalesForSpeculative, 
                      dbo.CustomerEquityNetPosition.CENP_CostofSalesforDelivery, dbo.CustomerEquityNetPosition.CENP_Deliverysaleproceeds, 
                      dbo.CustomerEquityNetPosition.CENP_Speculativesalesproceeds, dbo.CustomerEquityNetPosition.CENP_CurrentValue, 
                      dbo.CustomerEquityNetPosition.CENP_CreatedBy, dbo.CustomerEquityNetPosition.CENP_CreatedOn, 
                      dbo.CustomerEquityNetPosition.CENP_ModifiedBy, dbo.CustomerEquityNetPosition.CENP_ModifiedOn, 
                      dbo.CustomerEquityTradeAccount.CETA_AccountId AS Expr1, dbo.ProductAssetGroup.PAG_AssetGroupCode, 
                      dbo.ProductAssetGroup.PAG_AssetGroupName, dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode, 
                      dbo.ProductAssetInstrumentSubCategory.PAISC_AssetInstrumentSubCategoryCode, 
                      dbo.ProductAssetInstrumentSubCategory.PAISC_AssetInstrumentSubCategoryName, 
                      dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryName, dbo.ProductEquityMaster.PEM_Ticker
FROM         dbo.CustomerEquityTradeAccount INNER JOIN
                      dbo.CustomerEquityNetPosition ON dbo.CustomerEquityTradeAccount.CETA_AccountId = dbo.CustomerEquityNetPosition.CETA_AccountId INNER JOIN
                      dbo.CustomerPortfolio ON dbo.CustomerEquityTradeAccount.CP_PortfolioId = dbo.CustomerPortfolio.CP_PortfolioId INNER JOIN
                      dbo.Adviser INNER JOIN
                      dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId INNER JOIN
                      dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId ON 
                      dbo.CustomerPortfolio.C_CustomerId = dbo.Customer.C_CustomerId INNER JOIN
                      dbo.ProductAssetGroup ON dbo.CustomerEquityTradeAccount.PAG_AssetGroupCode = dbo.ProductAssetGroup.PAG_AssetGroupCode INNER JOIN
                      dbo.ProductAssetInstrumentCategory ON 
                      dbo.ProductAssetGroup.PAG_AssetGroupCode = dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode INNER JOIN
                      dbo.ProductAssetInstrumentSubCategory ON 
                      dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentSubCategory.PAIC_AssetInstrumentCategoryCode
                       AND dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode = dbo.ProductAssetInstrumentSubCategory.PAG_AssetGroupCode INNER JOIN
                      dbo.ProductEquityMaster ON dbo.CustomerEquityNetPosition.PEM_ScripCode = dbo.ProductEquityMaster.PEM_ScripCode AND 
                      dbo.ProductAssetInstrumentSubCategory.PAISC_AssetInstrumentSubCategoryCode = dbo.ProductEquityMaster.PAISC_AssetInstrumentSubCategoryCode
                       AND 
                      dbo.ProductAssetInstrumentSubCategory.PAIC_AssetInstrumentCategoryCode = dbo.ProductEquityMaster.PAIC_AssetInstrumentCategoryCode AND 
                      dbo.ProductAssetInstrumentSubCategory.PAG_AssetGroupCode = dbo.ProductEquityMaster.PAG_AssetGroupCode
' 
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'ViewEquityNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[24] 4[19] 2[28] 3) )"
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
         Begin Table = "CustomerEquityTradeAccount"
            Begin Extent = 
               Top = 126
               Left = 319
               Bottom = 241
               Right = 534
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerEquityNetPosition"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 241
               Right = 281
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerPortfolio"
            Begin Extent = 
               Top = 126
               Left = 572
               Bottom = 241
               Right = 743
            End
            DisplayFlags = 280
            TopColumn = 0
         End
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
         Begin Table = "ProductAssetGroup"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 361
               Rig' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewEquityNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'ViewEquityNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'ht = 228
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetInstrumentCategory"
            Begin Extent = 
               Top = 246
               Left = 266
               Bottom = 361
               Right = 529
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetInstrumentSubCategory"
            Begin Extent = 
               Top = 246
               Left = 567
               Bottom = 361
               Right = 854
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductEquityMaster"
            Begin Extent = 
               Top = 366
               Left = 387
               Bottom = 481
               Right = 672
            End
            DisplayFlags = 280
            TopColumn = 7
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 38
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewEquityNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'ViewEquityNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewEquityNP'
GO
