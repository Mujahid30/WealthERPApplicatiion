/****** Object:  View [dbo].[ViewGovtSavingsNP]    Script Date: 06/24/2009 14:45:24 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewGovtSavingsNP]'))
DROP VIEW [dbo].[ViewGovtSavingsNP]
GO
/****** Object:  View [dbo].[ViewGovtSavingsNP]    Script Date: 06/24/2009 14:45:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewGovtSavingsNP]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.ViewGovtSavingsNP
AS
SELECT     dbo.Adviser.A_AdviserId, dbo.AdviserRM.AR_RMId, dbo.Customer.C_CustomerId, dbo.CustomerGovtSavingNetPosition.CGSNP_GovtSavingNPId, 
                      dbo.CustomerGovtSavingNetPosition.CGSA_AccountId, dbo.CustomerGovtSavingNetPosition.PAIC_AssetInstrumentCategoryCode, 
                      dbo.CustomerGovtSavingNetPosition.PAG_AssetGroupCode, dbo.CustomerGovtSavingNetPosition.XDI_DebtIssuerCode, 
                      dbo.CustomerGovtSavingNetPosition.XIB_InterestBasisCode, dbo.CustomerGovtSavingNetPosition.XF_CompoundInterestFrequencyCode, 
                      dbo.CustomerGovtSavingNetPosition.XF_InterestPayableFrequencyCode, dbo.CustomerGovtSavingNetPosition.XF_DepositFrequencyCode, 
                      dbo.CustomerGovtSavingNetPosition.CGSNP_Name, dbo.CustomerGovtSavingNetPosition.CGSNP_PurchaseDate, 
                      dbo.CustomerGovtSavingNetPosition.CGSNP_Quantity, dbo.CustomerGovtSavingNetPosition.CGSNP_CurrentPrice, 
                      dbo.CustomerGovtSavingNetPosition.CGSNP_CurrentValue, dbo.CustomerGovtSavingNetPosition.CGSNP_MaturityDate, 
                      dbo.CustomerGovtSavingNetPosition.CGSNP_DepositAmount, dbo.CustomerGovtSavingNetPosition.CGSNP_MaturityValue, 
                      dbo.CustomerGovtSavingNetPosition.CGSNP_IsInterestAccumalated, dbo.CustomerGovtSavingNetPosition.CGSNP_InterestAmtAccumalated, 
                      dbo.CustomerGovtSavingNetPosition.CGSNP_InterestAmtPaidOut, dbo.CustomerGovtSavingNetPosition.CGSNP_InterestRate, 
                      dbo.CustomerGovtSavingNetPosition.CGSNP_Remark, dbo.CustomerGovtSavingNetPosition.CGSNP_SubsqntDepositAmount, 
                      dbo.CustomerGovtSavingNetPosition.CGSNP_SubsqntDepositDate, dbo.CustomerGovtSavingNetPosition.CGSNP_CreatedBy, 
                      dbo.CustomerGovtSavingNetPosition.CGSNP_CreatedOn, dbo.CustomerGovtSavingNetPosition.CGSNP_ModifiedBy, 
                      dbo.CustomerGovtSavingNetPosition.CGSNP_ModifiedOn, dbo.CustomerPortfolio.CP_PortfolioId, 
                      dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryName, dbo.ProductAssetGroup.PAG_AssetGroupName, 
                      dbo.CustomerGovtSavingAccount.CP_PortfolioId AS Expr1, dbo.CustomerGovtSavingAccount.CGSA_AccountNum, 
                      dbo.CustomerGovtSavingAccount.PAIC_AssetInstrumentCategoryCode AS Expr2, dbo.CustomerGovtSavingAccount.PAG_AssetGroupCode AS Expr3, 
                      dbo.CustomerGovtSavingAccount.CGSA_AccountSource, dbo.CustomerGovtSavingAccount.CGSA_IsHeldJointly, 
                      dbo.CustomerGovtSavingAccount.XMOH_ModeOfHoldingCode, dbo.CustomerGovtSavingAccount.CGSA_AccountOpeningDate
FROM         dbo.CustomerGovtSavingNetPosition INNER JOIN
                      dbo.CustomerGovtSavingAccount ON 
                      dbo.CustomerGovtSavingNetPosition.CGSA_AccountId = dbo.CustomerGovtSavingAccount.CGSA_AccountId INNER JOIN
                      dbo.CustomerPortfolio ON dbo.CustomerGovtSavingAccount.CP_PortfolioId = dbo.CustomerPortfolio.CP_PortfolioId INNER JOIN
                      dbo.Adviser INNER JOIN
                      dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId INNER JOIN
                      dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId ON 
                      dbo.CustomerPortfolio.C_CustomerId = dbo.Customer.C_CustomerId INNER JOIN
                      dbo.ProductAssetInstrumentCategory ON 
                      dbo.CustomerGovtSavingNetPosition.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode AND
                       dbo.CustomerGovtSavingNetPosition.PAG_AssetGroupCode = dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode AND 
                      dbo.CustomerGovtSavingAccount.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode AND 
                      dbo.CustomerGovtSavingAccount.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode AND 
                      dbo.CustomerGovtSavingAccount.PAG_AssetGroupCode = dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode AND 
                      dbo.CustomerGovtSavingAccount.PAG_AssetGroupCode = dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode INNER JOIN
                      dbo.ProductAssetGroup ON dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode = dbo.ProductAssetGroup.PAG_AssetGroupCode
' 
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'ViewGovtSavingsNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[57] 4[4] 2[20] 3) )"
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
         Begin Table = "CustomerGovtSavingNetPosition"
            Begin Extent = 
               Top = 126
               Left = 337
               Bottom = 241
               Right = 602
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerGovtSavingAccount"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 241
               Right = 299
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "CustomerPortfolio"
            Begin Extent = 
               Top = 126
               Left = 640
               Bottom = 241
               Right = 811
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
         Begin Table = "ProductAssetInstrumentCategory"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 361
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewGovtSavingsNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'ViewGovtSavingsNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'            Right = 301
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetGroup"
            Begin Extent = 
               Top = 246
               Left = 339
               Bottom = 354
               Right = 528
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewGovtSavingsNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'ViewGovtSavingsNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewGovtSavingsNP'
GO
