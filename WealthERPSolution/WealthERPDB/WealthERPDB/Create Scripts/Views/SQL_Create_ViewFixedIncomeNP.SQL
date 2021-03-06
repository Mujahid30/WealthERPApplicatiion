/****** Object:  View [dbo].[ViewFixedIncomeNP]    Script Date: 06/24/2009 14:45:19 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewFixedIncomeNP]'))
DROP VIEW [dbo].[ViewFixedIncomeNP]
GO
/****** Object:  View [dbo].[ViewFixedIncomeNP]    Script Date: 06/24/2009 14:45:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewFixedIncomeNP]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.ViewFixedIncomeNP
AS
SELECT     dbo.Adviser.A_AdviserId, dbo.AdviserRM.AR_RMId, dbo.Customer.C_CustomerId, dbo.CustomerPortfolio.CP_PortfolioId, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_FINPId, dbo.CustomerFixedIncomeNetPosition.CFIA_AccountId, 
                      dbo.CustomerFixedIncomeNetPosition.PAIC_AssetInstrumentCategoryCode, dbo.CustomerFixedIncomeNetPosition.PAG_AssetGroupCode, 
                      dbo.CustomerFixedIncomeNetPosition.XDI_DebtIssuerCode, dbo.CustomerFixedIncomeNetPosition.XIB_InterestBasisCode, 
                      dbo.CustomerFixedIncomeNetPosition.XF_CompoundInterestFrequencyCode, 
                      dbo.CustomerFixedIncomeNetPosition.XF_InterestPayableFrequencyCode, dbo.CustomerFixedIncomeNetPosition.CFINP_Name, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_IssueDate, dbo.CustomerFixedIncomeNetPosition.CFINP_PrincipalAmount, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_InterestAmtPaidOut, dbo.CustomerFixedIncomeNetPosition.CFINP_InterestAmtAcculumated, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_InterestRate, dbo.CustomerFixedIncomeNetPosition.CFINP_FaceValue, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_PurchasePrice, dbo.CustomerFixedIncomeNetPosition.CFINP_SubsequentDepositAmount, 
                      dbo.CustomerFixedIncomeNetPosition.XF_DepositFrquencycode, dbo.CustomerFixedIncomeNetPosition.CFINP_DebentureNum, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_PurchaseValue, dbo.CustomerFixedIncomeNetPosition.CFINP_PurchaseDate, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_MaturityDate, dbo.CustomerFixedIncomeNetPosition.CFINP_MaturityValue, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_IsInterestAccumulated, dbo.CustomerFixedIncomeNetPosition.CFINP_MaturityFaceValue, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_CurrentPrice, dbo.CustomerFixedIncomeNetPosition.CFINP_CurrentValue, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_Quantity, dbo.CustomerFixedIncomeNetPosition.CFINP_Remark, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_CreatedBy, dbo.CustomerFixedIncomeNetPosition.CFINP_CreatedOn, 
                      dbo.CustomerFixedIncomeNetPosition.CFINP_ModifiedBy, dbo.CustomerFixedIncomeNetPosition.CFINP_ModifiedOn, 
                      dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryName, dbo.ProductAssetGroup.PAG_AssetGroupName
FROM         dbo.Adviser INNER JOIN
                      dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId INNER JOIN
                      dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId INNER JOIN
                      dbo.CustomerPortfolio ON dbo.Customer.C_CustomerId = dbo.CustomerPortfolio.C_CustomerId INNER JOIN
                      dbo.CustomerFixedIncomeAccount ON dbo.CustomerPortfolio.CP_PortfolioId = dbo.CustomerFixedIncomeAccount.CP_PortfolioId INNER JOIN
                      dbo.CustomerFixedIncomeNetPosition ON 
                      dbo.CustomerFixedIncomeAccount.CFIA_AccountId = dbo.CustomerFixedIncomeNetPosition.CFIA_AccountId INNER JOIN
                      dbo.ProductAssetInstrumentCategory ON 
                      dbo.CustomerFixedIncomeAccount.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode AND
                       dbo.CustomerFixedIncomeAccount.PAG_AssetGroupCode = dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode AND 
                      dbo.CustomerFixedIncomeNetPosition.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode AND
                       dbo.CustomerFixedIncomeNetPosition.PAG_AssetGroupCode = dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode INNER JOIN
                      dbo.ProductAssetGroup ON dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode = dbo.ProductAssetGroup.PAG_AssetGroupCode
' 
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'ViewFixedIncomeNP', NULL,NULL))
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
         Begin Table = "Adviser"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 259
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdviserRM"
            Begin Extent = 
               Top = 114
               Left = 38
               Bottom = 222
               Right = 244
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Customer"
            Begin Extent = 
               Top = 222
               Left = 38
               Bottom = 330
               Right = 262
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerPortfolio"
            Begin Extent = 
               Top = 6
               Left = 297
               Bottom = 114
               Right = 467
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerFixedIncomeAccount"
            Begin Extent = 
               Top = 330
               Left = 38
               Bottom = 438
               Right = 298
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerFixedIncomeNetPosition"
            Begin Extent = 
               Top = 438
               Left = 38
               Bottom = 546
               Right = 302
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetInstrumentCategory"
            Begin Extent = 
               Top = 546
               Left = 38
               Bottom = 654
 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewFixedIncomeNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'ViewFixedIncomeNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'              Right = 300
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAssetGroup"
            Begin Extent = 
               Top = 6
               Left = 505
               Bottom = 114
               Right = 694
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewFixedIncomeNP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'ViewFixedIncomeNP', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewFixedIncomeNP'
GO
