/****** Object:  View [dbo].[ViewPensionGratuities]    Script Date: 06/24/2009 14:45:31 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewPensionGratuities]'))
DROP VIEW [dbo].[ViewPensionGratuities]
GO
/****** Object:  View [dbo].[ViewPensionGratuities]    Script Date: 06/24/2009 14:45:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewPensionGratuities]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.ViewPensionGratuities
AS
SELECT     dbo.Adviser.A_AdviserId, dbo.AdviserRM.AR_RMId, dbo.Customer.C_CustomerId, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_PensionGratutiesNPId, dbo.CustomerPensionandGratuitiesNetPosition.CPGA_AccountId, 
                      dbo.CustomerPensionandGratuitiesNetPosition.PAIC_AssetInstrumentCategoryCode, 
                      dbo.CustomerPensionandGratuitiesNetPosition.PAG_AssetGroupCode, dbo.CustomerPensionandGratuitiesNetPosition.XDI_DebtIssuerCode, 
                      dbo.CustomerPensionandGratuitiesNetPosition.XFY_FiscalYearCode, dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_EmployeeContri, 
                      dbo.CustomerPensionandGratuitiesNetPosition.XF_InterestPayableFrequencyCode, 
                      dbo.CustomerPensionandGratuitiesNetPosition.XF_CompoundInterestFrequencyCode, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_InterestRate, dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_OrganizationName, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_PurchaseDate, dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_DepositAmount, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_EmployerContri, dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_MaturityDate, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_MaturityValue, dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_CurrentValue, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_Remark, dbo.CustomerPensionandGratuitiesNetPosition.XIB_InterestBasisCode, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_IsInterestAccumalated, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_InterestAmtAccumalated, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_InterestAmtPaidOut, dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_LoanStartDate, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_LoanEndDate, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_LoanOutstandingAmount, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_LoanDescription, dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_CreatedBy, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_CreatedOn, dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_ModifiedBy, 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGNP_ModifiedOn, dbo.CustomerPortfolio.CP_PortfolioId, 
                      dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryName, dbo.ProductAssetGroup.PAG_AssetGroupName
FROM         dbo.CustomerPensionandGratuitiesNetPosition INNER JOIN
                      dbo.CustomerPensionandGratuitiesAccount ON 
                      dbo.CustomerPensionandGratuitiesNetPosition.CPGA_AccountId = dbo.CustomerPensionandGratuitiesAccount.CPGA_AccountId INNER JOIN
                      dbo.CustomerPortfolio ON dbo.CustomerPensionandGratuitiesAccount.CP_PortfolioId = dbo.CustomerPortfolio.CP_PortfolioId INNER JOIN
                      dbo.Adviser INNER JOIN
                      dbo.AdviserRM ON dbo.Adviser.A_AdviserId = dbo.AdviserRM.A_AdviserId INNER JOIN
                      dbo.Customer ON dbo.AdviserRM.AR_RMId = dbo.Customer.AR_RMId ON 
                      dbo.CustomerPortfolio.C_CustomerId = dbo.Customer.C_CustomerId INNER JOIN
                      dbo.ProductAssetInstrumentCategory ON 
                      dbo.CustomerPensionandGratuitiesNetPosition.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode
                       AND dbo.CustomerPensionandGratuitiesNetPosition.PAG_AssetGroupCode = dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode AND 
                      dbo.CustomerPensionandGratuitiesAccount.PAIC_AssetInstrumentCategoryCode = dbo.ProductAssetInstrumentCategory.PAIC_AssetInstrumentCategoryCode
                       AND dbo.CustomerPensionandGratuitiesAccount.PAG_AssetGroupCode = dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode INNER JOIN
                      dbo.ProductAssetGroup ON dbo.ProductAssetInstrumentCategory.PAG_AssetGroupCode = dbo.ProductAssetGroup.PAG_AssetGroupCode
' 
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'ViewPensionGratuities', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[60] 4[2] 2[20] 3) )"
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
         Begin Table = "CustomerPensionandGratuitiesNetPosition"
            Begin Extent = 
               Top = 126
               Left = 337
               Bottom = 241
               Right = 602
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CustomerPensionandGratuitiesAccount"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 241
               Right = 299
            End
            DisplayFlags = 280
            TopColumn = 0
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
            ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPensionGratuities'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'ViewPensionGratuities', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'   Bottom = 361
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPensionGratuities'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'ViewPensionGratuities', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPensionGratuities'
GO
