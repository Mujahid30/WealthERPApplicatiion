/****** Object:  View [dbo].[ViewPersonalNP]    Script Date: 06/24/2009 14:45:34 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewPersonalNP]'))
DROP VIEW [dbo].[ViewPersonalNP]
GO
/****** Object:  View [dbo].[ViewPersonalNP]    Script Date: 06/24/2009 14:45:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewPersonalNP]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.ViewPersonalNP
AS
SELECT     dbo.Adviser.A_AdviserId, dbo.AdviserRM.AR_RMId, dbo.Customer.C_CustomerId, dbo.CustomerPortfolio.CP_PortfolioId AS Expr1, 
                      dbo.CustomerPersonalNetPosition.CPNP_PersonalNPId, dbo.CustomerPersonalNetPosition.CP_PortfolioId, 
                      dbo.CustomerPersonalNetPosition.PAISC_AssetInstrumentSubCategoryCode, dbo.CustomerPersonalNetPosition.PAIC_AssetInstrumentCategoryCode, 
                      dbo.CustomerPersonalNetPosition.PAG_AssetGroupCode, dbo.CustomerPersonalNetPosition.CPNP_Name, 
                      dbo.CustomerPersonalNetPosition.CPNP_PurchaseDate, dbo.CustomerPersonalNetPosition.CPNP_Quantity, 
                      dbo.CustomerPersonalNetPosition.CPNP_PurchasePrice, dbo.CustomerPersonalNetPosition.CPNP_PurchaseValue, 
                      dbo.CustomerPersonalNetPosition.CPNP_CurrentPrice, dbo.CustomerPersonalNetPosition.CPNP_CurrentValue, 
                      dbo.CustomerPersonalNetPosition.CPNP_CreatedBy, dbo.CustomerPersonalNetPosition.CPNP_CreatedOn, 
                      dbo.CustomerPersonalNetPosition.CPNP_ModifiedBy, dbo.CustomerPersonalNetPosition.CPNP_ModifiedOn, 
                      dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryName, 
                      dbo.ProductAssetInstrumentSubCategory.PAISC_AssetInstrumentSubCategoryName, dbo.ProductAssetGroup.PAG_AssetGroupName
FROM         dbo.Adviser INNER JOIN
                      dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId INNER JOIN
                      dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId INNER JOIN
                      dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId INNER JOIN
                      dbo.CustomerPersonalNetPosition ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerPersonalNetPosition.CP_PortfolioId INNER JOIN
                      dbo.ProductAssetInstrumentCategory ON 
                      dbo.CustomerPersonalNetPosition.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode INNER
                       JOIN
                      dbo.ProductAssetInstrumentSubCategory ON 
                      dbo.CustomerPersonalNetPosition.PAISC_AssetInstrumentSubCategoryCode = dbo.ProductAssetInstrumentSubCategory.PAISC_AssetInstrumentSubCategoryCode
                       AND 
                      dbo.CustomerPersonalNetPosition.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentSubCategory.PAIC_AssetInstrumentCategoryCode AND
                       dbo.CustomerPersonalNetPosition.PAG_AssetGroupCode = dbo.ProductAssetInstrumentSubCategory.PAG_AssetGroupCode AND 
                      dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentSubCategory.PAIC_AssetInstrumentCategoryCode
                       AND dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode = dbo.ProductAssetInstrumentSubCategory.PAG_AssetGroupCode INNER JOIN
                      dbo.ProductAssetGroup ON dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode = dbo.ProductAssetGroup.PAG_AssetGroupCode
' 
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'ViewPersonalNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[59] 4[3] 2[20] 3) )"
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
               Left = 361
               Bottom = 241
               Right = 532
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerPersonalNetPosition"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 241
               Right = 323
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetInstrumentCategory"
            Begin Extent = 
               Top = 126
               Left = 570
               Bottom = 241
               Right = 833
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetInstrumentSubCategory"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 36' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPersonalNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'ViewPersonalNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'1
               Right = 325
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetGroup"
            Begin Extent = 
               Top = 246
               Left = 363
               Bottom = 354
               Right = 552
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPersonalNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'ViewPersonalNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPersonalNP'
GO
