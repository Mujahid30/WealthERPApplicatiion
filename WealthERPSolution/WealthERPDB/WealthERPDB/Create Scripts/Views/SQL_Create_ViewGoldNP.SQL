/****** Object:  View [dbo].[ViewGoldNP]    Script Date: 06/24/2009 14:45:21 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewGoldNP]'))
DROP VIEW [dbo].[ViewGoldNP]
GO
/****** Object:  View [dbo].[ViewGoldNP]    Script Date: 06/24/2009 14:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewGoldNP]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.ViewGoldNP
AS
SELECT     dbo.Adviser.A_AdviserId, dbo.AdviserRM.AR_RMId, dbo.CustomerPortfolio.CP_PortfolioId AS Expr1, dbo.Customer.C_CustomerId, 
                      dbo.CustomerGoldNetPosition.CGNP_GoldNPId, dbo.CustomerGoldNetPosition.CP_PortfolioId, 
                      dbo.CustomerGoldNetPosition.PAIC_AssetInstrumentCategoryCode, dbo.CustomerGoldNetPosition.PAG_AssetGroupCode, 
                      dbo.CustomerGoldNetPosition.XMC_MeasureCode, dbo.CustomerGoldNetPosition.CGNP_Name, dbo.CustomerGoldNetPosition.CGNP_PurchaseDate, 
                      dbo.CustomerGoldNetPosition.CGNP_PurchasePrice, dbo.CustomerGoldNetPosition.CGNP_Quantity, 
                      dbo.CustomerGoldNetPosition.CGNP_PurchaseValue, dbo.CustomerGoldNetPosition.CGNP_CurrentPrice, 
                      dbo.CustomerGoldNetPosition.CGNP_CurrentValue, dbo.CustomerGoldNetPosition.CGNP_SellDate, dbo.CustomerGoldNetPosition.CGNP_SellPrice, 
                      dbo.CustomerGoldNetPosition.CGNP_SellValue, dbo.CustomerGoldNetPosition.CGNP_Remark, dbo.CustomerGoldNetPosition.CGNP_CreatedBy, 
                      dbo.CustomerGoldNetPosition.CGNP_CreatedOn, dbo.CustomerGoldNetPosition.CGNP_ModifiedBy, dbo.CustomerGoldNetPosition.CGNP_ModifiedOn, 
                      dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryName, dbo.ProductAssetGroup.PAG_AssetGroupName
FROM         dbo.Adviser INNER JOIN
                      dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId INNER JOIN
                      dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId INNER JOIN
                      dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId INNER JOIN
                      dbo.CustomerGoldNetPosition ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerGoldNetPosition.CP_PortfolioId INNER JOIN
                      dbo.ProductAssetInstrumentCategory ON 
                      dbo.CustomerGoldNetPosition.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode AND 
                      dbo.CustomerGoldNetPosition.PAG_AssetGroupCode = dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode INNER JOIN
                      dbo.ProductAssetGroup ON dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode = dbo.ProductAssetGroup.PAG_AssetGroupCode
' 
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'ViewGoldNP', NULL,NULL))
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
         Top = -96
         Left = 0
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
               Top = 126
               Left = 38
               Bottom = 241
               Right = 263
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerPortfolio"
            Begin Extent = 
               Top = 6
               Left = 543
               Bottom = 121
               Right = 714
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerGoldNetPosition"
            Begin Extent = 
               Top = 126
               Left = 301
               Bottom = 241
               Right = 562
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetInstrumentCategory"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 361
               Right = 301
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetGroup"
            Begin Extent = 
               Top = 246
               Left = 339
               Bottom = 354
              ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewGoldNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'ViewGoldNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' Right = 528
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewGoldNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'ViewGoldNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewGoldNP'
GO
