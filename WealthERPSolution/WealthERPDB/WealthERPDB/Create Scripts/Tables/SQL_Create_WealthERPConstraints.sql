 /****** Object:  Default [DF_TABLE_EVENT_LOOKUP_Reminder]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AlertEventLookup] ADD  CONSTRAINT [DF_TABLE_EVENT_LOOKUP_Reminder]  DEFAULT ((0)) FOR [AEL_Reminder]
GO
/****** Object:  Default [DF_TABLE_EVENTS_SETUP_SentToQueue]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AlertEventSetup] ADD  CONSTRAINT [DF_TABLE_EVENTS_SETUP_SentToQueue]  DEFAULT ((0)) FOR [AES_SentToQueue]
GO
/****** Object:  Default [DF_CustomerInvestmentCollectiblePortfolio_CICP_PurchasePrice]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCollectibleNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentCollectiblePortfolio_CICP_PurchasePrice]  DEFAULT ((0)) FOR [CCNP_PurchasePrice]
GO
/****** Object:  Default [DF_CustomerInvestmentCollectiblePortfolio_CICP_PurchaseValue]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCollectibleNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentCollectiblePortfolio_CICP_PurchaseValue]  DEFAULT ((0)) FOR [CCNP_PurchaseValue]
GO
/****** Object:  Default [DF_CustomerInvestmentCollectiblePortfolio_CICP_CurrentPrice]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCollectibleNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentCollectiblePortfolio_CICP_CurrentPrice]  DEFAULT ((0)) FOR [CCNP_CurrentPrice]
GO
/****** Object:  Default [DF_CustomerInvestmentCollectiblePortfolio_CICP_CurrentValue]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCollectibleNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentCollectiblePortfolio_CICP_CurrentValue]  DEFAULT ((0)) FOR [CCNP_CurrentValue]
GO
/****** Object:  Default [DF_CustomerAssetGroupEquityAccount_PAG_AssetGroupCode]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTradeAccount] ADD  CONSTRAINT [DF_CustomerAssetGroupEquityAccount_PAG_AssetGroupCode]  DEFAULT ('DE') FOR [PAG_AssetGroupCode]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_TradeNum]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_TradeNum]  DEFAULT ((0)) FOR [CET_TradeNum]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_OrderNum]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_OrderNum]  DEFAULT ((0)) FOR [CET_OrderNum]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_Rate]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_Rate]  DEFAULT ((0)) FOR [CET_Rate]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_Quantity]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_Quantity]  DEFAULT ((0)) FOR [CET_Quantity]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_Brokerage]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_Brokerage]  DEFAULT ((0)) FOR [CET_Brokerage]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_ServiceTax]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_ServiceTax]  DEFAULT ((0)) FOR [CET_ServiceTax]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_EducationCess]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_EducationCess]  DEFAULT ((0)) FOR [CET_EducationCess]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_STT]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_STT]  DEFAULT ((0)) FOR [CET_STT]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_OtherCharges]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_OtherCharges]  DEFAULT ((0)) FOR [CET_OtherCharges]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_RateInclBrokerage]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_RateInclBrokerage]  DEFAULT ((0)) FOR [CET_RateInclBrokerage]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_TradeTotal]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_TradeTotal]  DEFAULT ((0)) FOR [CET_TradeTotal]
GO
/****** Object:  Default [DF_CustomerEquityTransaction_CET_IsSplit]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerEquityTransaction_CET_IsSplit]  DEFAULT ((0)) FOR [CET_IsSplit]
GO
/****** Object:  Default [DF_CustomerInvestmentEquityTransaction_CIET_SplitCustEqTransId]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_SplitCustEqTransId]  DEFAULT ((0)) FOR [CET_SplitCustEqTransId]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_PrincipalAmount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_PrincipalAmount]  DEFAULT ((0)) FOR [CFINP_PrincipalAmount]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_InterestAmtPaidOut]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_InterestAmtPaidOut]  DEFAULT ((0)) FOR [CFINP_InterestAmtPaidOut]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_InterestAmtAcculumated]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_InterestAmtAcculumated]  DEFAULT ((0)) FOR [CFINP_InterestAmtAcculumated]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_InterestRate]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_InterestRate]  DEFAULT ((0)) FOR [CFINP_InterestRate]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_FaceValue]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_FaceValue]  DEFAULT ((0)) FOR [CFINP_FaceValue]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_PurchasePrice]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_PurchasePrice]  DEFAULT ((0)) FOR [CFINP_PurchasePrice]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_SubsequentDepositAmount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_SubsequentDepositAmount]  DEFAULT ((0)) FOR [CFINP_SubsequentDepositAmount]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_DebentureNum]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_DebentureNum]  DEFAULT ((0)) FOR [CFINP_DebentureNum]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_PurchaseValue]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_PurchaseValue]  DEFAULT ((0)) FOR [CFINP_PurchaseValue]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_MaturityValue]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_MaturityValue]  DEFAULT ((0)) FOR [CFINP_MaturityValue]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_MaturityFaceValue]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_MaturityFaceValue]  DEFAULT ((0)) FOR [CFINP_MaturityFaceValue]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_CurrentPrice]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_CurrentPrice]  DEFAULT ((0)) FOR [CFINP_CurrentPrice]
GO
/****** Object:  Default [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_CurrentValue]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_CurrentValue]  DEFAULT ((0)) FOR [CFINP_CurrentValue]
GO
/****** Object:  Default [DF_CustomerMFCAMSXtrnlProfileInput_CMGCXPI_EMAIL]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMFCAMSXtrnlProfileInput] ADD  CONSTRAINT [DF_CustomerMFCAMSXtrnlProfileInput_CMGCXPI_EMAIL]  DEFAULT (' ') FOR [CMGCXPI_EMAIL]
GO
/****** Object:  Default [DF_CustomerInvestmentMFCAMSXtrnlStaging_CCMT_StatusCode]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMFCAMSXtrnlTransactionStaging] ADD  CONSTRAINT [DF_CustomerInvestmentMFCAMSXtrnlStaging_CCMT_StatusCode]  DEFAULT ((0)) FOR [CMCXTS_StatusCode]
GO
/****** Object:  Default [DF_CustomerAssetGroupMutualFundAccount_PAG_AssetGroupCode]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundAccount] ADD  CONSTRAINT [DF_CustomerAssetGroupMutualFundAccount_PAG_AssetGroupCode]  DEFAULT ('MF') FOR [PAG_AssetGroupCode]
GO
/****** Object:  Default [DF_CustomerInvestmentMutualFundTransaction_CIMFT_DividendRate]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentMutualFundTransaction_CIMFT_DividendRate]  DEFAULT ((0)) FOR [CMFT_DividendRate]
GO
/****** Object:  Default [DF_CustomerMutualFundTransaction_CMFT_NAV]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CMFT_NAV]  DEFAULT ((0)) FOR [CMFT_NAV]
GO
/****** Object:  Default [DF_CustomerMutualFundTransaction_CMFT_Price]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CMFT_Price]  DEFAULT ((0)) FOR [CMFT_Price]
GO
/****** Object:  Default [DF_CustomerMutualFundTransaction_CMFT_Amount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CMFT_Amount]  DEFAULT ((0)) FOR [CMFT_Amount]
GO
/****** Object:  Default [DF_CustomerMutualFundTransaction_CMFT_Units]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CMFT_Units]  DEFAULT ((0)) FOR [CMFT_Units]
GO
/****** Object:  Default [DF_CustomerInvestmentMutualFundTransaction_CIMFT_STT]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentMutualFundTransaction_CIMFT_STT]  DEFAULT ((0)) FOR [CMFT_STT]
GO
/****** Object:  Default [DF_CustomerInvestmentMutualFundTransaction_CIMFT_SwitchSourceTrxId]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentMutualFundTransaction_CIMFT_SwitchSourceTrxId]  DEFAULT ((0)) FOR [CMFT_SwitchSourceTrxId]
GO
/****** Object:  Default [DF_CustomerMutualFundTransaction_CIMFT_ModifiedBy]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CIMFT_ModifiedBy]  DEFAULT ((0)) FOR [CMFT_ModifiedBy]
GO
/****** Object:  Default [DF_CustomerMutualFundTransaction_CIMFT_CreatedBy]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CIMFT_CreatedBy]  DEFAULT ((0)) FOR [CMFT_CreatedBy]
GO
/****** Object:  ForeignKey [FK_Adviser_User]    Script Date: 06/12/2009 18:44:51 ******/
ALTER TABLE [dbo].[Adviser]  WITH CHECK ADD  CONSTRAINT [FK_Adviser_User] FOREIGN KEY([U_UserId])
REFERENCES [dbo].[User] ([U_UserId])
GO
ALTER TABLE [dbo].[Adviser] CHECK CONSTRAINT [FK_Adviser_User]
GO
/****** Object:  ForeignKey [FK_Adviser_XMLAdviserBusinessType]    Script Date: 06/12/2009 18:44:51 ******/
ALTER TABLE [dbo].[Adviser]  WITH CHECK ADD  CONSTRAINT [FK_Adviser_XMLAdviserBusinessType] FOREIGN KEY([XABT_BusinessTypeCode])
REFERENCES [dbo].[XMLAdviserBusinessType] ([XABT_BusinessTypeCode])
GO
ALTER TABLE [dbo].[Adviser] CHECK CONSTRAINT [FK_Adviser_XMLAdviserBusinessType]
GO
/****** Object:  ForeignKey [FK_AdvisorAssetClass_AdvisorMaster]    Script Date: 06/12/2009 18:44:51 ******/
ALTER TABLE [dbo].[AdviserAssetClasses]  WITH NOCHECK ADD  CONSTRAINT [FK_AdvisorAssetClass_AdvisorMaster] FOREIGN KEY([A_AdvisorId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO
ALTER TABLE [dbo].[AdviserAssetClasses] CHECK CONSTRAINT [FK_AdvisorAssetClass_AdvisorMaster]
GO
/****** Object:  ForeignKey [FK_AdviserBranch_Adviser]    Script Date: 06/12/2009 18:44:51 ******/
ALTER TABLE [dbo].[AdviserBranch]  WITH CHECK ADD  CONSTRAINT [FK_AdviserBranch_Adviser] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO
ALTER TABLE [dbo].[AdviserBranch] CHECK CONSTRAINT [FK_AdviserBranch_Adviser]
GO
/****** Object:  ForeignKey [FK_AdviserBranch_AdviserRM]    Script Date: 06/12/2009 18:44:51 ******/
ALTER TABLE [dbo].[AdviserBranch]  WITH CHECK ADD  CONSTRAINT [FK_AdviserBranch_AdviserRM] FOREIGN KEY([AB_BranchHeadId])
REFERENCES [dbo].[AdviserRM] ([AR_RMId])
GO
ALTER TABLE [dbo].[AdviserBranch] CHECK CONSTRAINT [FK_AdviserBranch_AdviserRM]
GO
/****** Object:  ForeignKey [FK_AdviserDailyEODLog_Adviser]    Script Date: 06/12/2009 18:44:51 ******/
ALTER TABLE [dbo].[AdviserDailyEODLog]  WITH CHECK ADD  CONSTRAINT [FK_AdviserDailyEODLog_Adviser] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO
ALTER TABLE [dbo].[AdviserDailyEODLog] CHECK CONSTRAINT [FK_AdviserDailyEODLog_Adviser]
GO
/****** Object:  ForeignKey [FK_AdviserDailyEODLog_AdviserDailyEODLog]    Script Date: 06/12/2009 18:44:51 ******/
ALTER TABLE [dbo].[AdviserDailyEODLog]  WITH CHECK ADD  CONSTRAINT [FK_AdviserDailyEODLog_AdviserDailyEODLog] FOREIGN KEY([ADEL_EODLogId])
REFERENCES [dbo].[AdviserDailyEODLog] ([ADEL_EODLogId])
GO
ALTER TABLE [dbo].[AdviserDailyEODLog] CHECK CONSTRAINT [FK_AdviserDailyEODLog_AdviserDailyEODLog]
GO
/****** Object:  ForeignKey [FK_AdviserDailyUploadLog_Adviser]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserDailyUploadLog]  WITH CHECK ADD  CONSTRAINT [FK_AdviserDailyUploadLog_Adviser] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO
ALTER TABLE [dbo].[AdviserDailyUploadLog] CHECK CONSTRAINT [FK_AdviserDailyUploadLog_Adviser]
GO
/****** Object:  ForeignKey [FK_AdviserDailyUploadLog_XMLExternalSourceFileType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserDailyUploadLog]  WITH CHECK ADD  CONSTRAINT [FK_AdviserDailyUploadLog_XMLExternalSourceFileType] FOREIGN KEY([XESFT_FileTypeId])
REFERENCES [dbo].[XMLExternalSourceFileType] ([XESFT_FileTypeId])
GO
ALTER TABLE [dbo].[AdviserDailyUploadLog] CHECK CONSTRAINT [FK_AdviserDailyUploadLog_XMLExternalSourceFileType]
GO
/****** Object:  ForeignKey [FK_WerpUploadLog_User]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserDailyUploadLog]  WITH CHECK ADD  CONSTRAINT [FK_WerpUploadLog_User] FOREIGN KEY([U_UserId])
REFERENCES [dbo].[User] ([U_UserId])
GO
ALTER TABLE [dbo].[AdviserDailyUploadLog] CHECK CONSTRAINT [FK_WerpUploadLog_User]
GO
/****** Object:  ForeignKey [FK_AdviserEquityBrokerage_Adviser]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserEquityBrokerage]  WITH CHECK ADD  CONSTRAINT [FK_AdviserEquityBrokerage_Adviser] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO
ALTER TABLE [dbo].[AdviserEquityBrokerage] CHECK CONSTRAINT [FK_AdviserEquityBrokerage_Adviser]
GO
/****** Object:  ForeignKey [FK_AdviserLOB_XMLAdviserLOBClassification]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserLOB]  WITH CHECK ADD  CONSTRAINT [FK_AdviserLOB_XMLAdviserLOBClassification] FOREIGN KEY([XALC_LOBClassificationCode])
REFERENCES [dbo].[XMLAdviserLOBClassification] ([XALC_LOBClassificationCode])
GO
ALTER TABLE [dbo].[AdviserLOB] CHECK CONSTRAINT [FK_AdviserLOB_XMLAdviserLOBClassification]
GO
/****** Object:  ForeignKey [FK_AdviserLOB_XMLAdviserLOBIdentifierType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserLOB]  WITH CHECK ADD  CONSTRAINT [FK_AdviserLOB_XMLAdviserLOBIdentifierType] FOREIGN KEY([XALIT_IdentifierTypeCode])
REFERENCES [dbo].[XMLAdviserLOBIdentifierType] ([XALIT_IdentifierTypeCode])
GO
ALTER TABLE [dbo].[AdviserLOB] CHECK CONSTRAINT [FK_AdviserLOB_XMLAdviserLOBIdentifierType]
GO
/****** Object:  ForeignKey [FK_AdvisorLOB_AdvisorMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserLOB]  WITH CHECK ADD  CONSTRAINT [FK_AdvisorLOB_AdvisorMaster] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO
ALTER TABLE [dbo].[AdviserLOB] CHECK CONSTRAINT [FK_AdvisorLOB_AdvisorMaster]
GO
/****** Object:  ForeignKey [FK_RMMaster_AdvisorMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserRM]  WITH CHECK ADD  CONSTRAINT [FK_RMMaster_AdvisorMaster] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO
ALTER TABLE [dbo].[AdviserRM] CHECK CONSTRAINT [FK_RMMaster_AdvisorMaster]
GO
/****** Object:  ForeignKey [FK_RMMaster_UserMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserRM]  WITH CHECK ADD  CONSTRAINT [FK_RMMaster_UserMaster] FOREIGN KEY([U_UserId])
REFERENCES [dbo].[User] ([U_UserId])
GO
ALTER TABLE [dbo].[AdviserRM] CHECK CONSTRAINT [FK_RMMaster_UserMaster]
GO
/****** Object:  ForeignKey [FK_RMBranch_AdvisorBranch]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserRMBranch]  WITH CHECK ADD  CONSTRAINT [FK_RMBranch_AdvisorBranch] FOREIGN KEY([AB_BranchId])
REFERENCES [dbo].[AdviserBranch] ([AB_BranchId])
GO
ALTER TABLE [dbo].[AdviserRMBranch] CHECK CONSTRAINT [FK_RMBranch_AdvisorBranch]
GO
/****** Object:  ForeignKey [FK_RMBranch_RMMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserRMBranch]  WITH CHECK ADD  CONSTRAINT [FK_RMBranch_RMMaster] FOREIGN KEY([AR_RMId])
REFERENCES [dbo].[AdviserRM] ([AR_RMId])
GO
ALTER TABLE [dbo].[AdviserRMBranch] CHECK CONSTRAINT [FK_RMBranch_RMMaster]
GO
/****** Object:  ForeignKey [FK_AdvisorTerminal_AdvisorBranch]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserTerminal]  WITH CHECK ADD  CONSTRAINT [FK_AdvisorTerminal_AdvisorBranch] FOREIGN KEY([AB_BranchId])
REFERENCES [dbo].[AdviserBranch] ([AB_BranchId])
GO
ALTER TABLE [dbo].[AdviserTerminal] CHECK CONSTRAINT [FK_AdvisorTerminal_AdvisorBranch]
GO
/****** Object:  ForeignKey [FK_AdvisorTerminal_AdvisorTerminal]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[AdviserTerminal]  WITH CHECK ADD  CONSTRAINT [FK_AdvisorTerminal_AdvisorTerminal] FOREIGN KEY([AT_Id])
REFERENCES [dbo].[AdviserTerminal] ([AT_Id])
GO
ALTER TABLE [dbo].[AdviserTerminal] CHECK CONSTRAINT [FK_AdvisorTerminal_AdvisorTerminal]
GO
/****** Object:  ForeignKey [FK_Customer_XMLCustomerSubType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLCustomerSubType] FOREIGN KEY([XCST_CustomerSubTypeCode])
REFERENCES [dbo].[XMLCustomerSubType] ([XCST_CustomerSubTypeCode])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLCustomerSubType]
GO
/****** Object:  ForeignKey [FK_Customer_XMLCustomerType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLCustomerType] FOREIGN KEY([XCT_CustomerTypeCode])
REFERENCES [dbo].[XMLCustomerType] ([XCT_CustomerTypeCode])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLCustomerType]
GO
/****** Object:  ForeignKey [FK_Customer_XMLMaritalStatus]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLMaritalStatus] FOREIGN KEY([XMS_MaritalStatusCode])
REFERENCES [dbo].[XMLMaritalStatus] ([XMS_MaritalStatusCode])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLMaritalStatus]
GO
/****** Object:  ForeignKey [FK_Customer_XMLNationality]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLNationality] FOREIGN KEY([XN_NationalityCode])
REFERENCES [dbo].[XMLNationality] ([XN_NationalityCode])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLNationality]
GO
/****** Object:  ForeignKey [FK_Customer_XMLOccupation]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLOccupation] FOREIGN KEY([XO_OccupationCode])
REFERENCES [dbo].[XMLOccupation] ([XO_OccupationCode])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLOccupation]
GO
/****** Object:  ForeignKey [FK_Customer_XMLQualification]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_XMLQualification] FOREIGN KEY([XQ_QualificationCode])
REFERENCES [dbo].[XMLQualification] ([XQ_QualificationCode])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_XMLQualification]
GO
/****** Object:  ForeignKey [FK_CustomerMaster_RMMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMaster_RMMaster] FOREIGN KEY([AR_RMId])
REFERENCES [dbo].[AdviserRM] ([AR_RMId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_CustomerMaster_RMMaster]
GO
/****** Object:  ForeignKey [FK_CustomerMaster_UserMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMaster_UserMaster] FOREIGN KEY([U_UMId])
REFERENCES [dbo].[User] ([U_UserId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_CustomerMaster_UserMaster]
GO
/****** Object:  ForeignKey [FK_CustomerAssociates_Customer]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAssociates_Customer] FOREIGN KEY([C_AssociateCustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO
ALTER TABLE [dbo].[CustomerAssociates] CHECK CONSTRAINT [FK_CustomerAssociates_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerAssociates_XMLRelationship]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAssociates_XMLRelationship] FOREIGN KEY([XR_RelationshipCode])
REFERENCES [dbo].[XMLRelationship] ([XR_RelationshipCode])
GO
ALTER TABLE [dbo].[CustomerAssociates] CHECK CONSTRAINT [FK_CustomerAssociates_XMLRelationship]
GO
/****** Object:  ForeignKey [FK_CustomerFamily_CustomerMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFamily_CustomerMaster] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO
ALTER TABLE [dbo].[CustomerAssociates] CHECK CONSTRAINT [FK_CustomerFamily_CustomerMaster]
GO
/****** Object:  ForeignKey [FK_CustomerBank_XMLBankAccountType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerBank]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBank_XMLBankAccountType] FOREIGN KEY([XBAT_BankAccountTypeCode])
REFERENCES [dbo].[XMLBankAccountType] ([XBAT_BankAccountTypeCode])
GO
ALTER TABLE [dbo].[CustomerBank] CHECK CONSTRAINT [FK_CustomerBank_XMLBankAccountType]
GO
/****** Object:  ForeignKey [FK_CustomerBank_XMLModeOfHolding]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerBank]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBank_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO
ALTER TABLE [dbo].[CustomerBank] CHECK CONSTRAINT [FK_CustomerBank_XMLModeOfHolding]
GO
/****** Object:  ForeignKey [FK_CustomerBankAccount_CustomerMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerBank]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBankAccount_CustomerMaster] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO
ALTER TABLE [dbo].[CustomerBank] CHECK CONSTRAINT [FK_CustomerBankAccount_CustomerMaster]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsAccount_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerCashSavingsAccount] CHECK CONSTRAINT [FK_CustomerCashSavingsAccount_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsAccount_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsAccount_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerCashSavingsAccount] CHECK CONSTRAINT [FK_CustomerCashSavingsAccount_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsAccount_XMLModeOfHolding]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO
ALTER TABLE [dbo].[CustomerCashSavingsAccount] CHECK CONSTRAINT [FK_CustomerCashSavingsAccount_XMLModeOfHolding]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsAccountAssociates_CustomerAssociates]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsAccountAssociates]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerCashSavingsAccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO
ALTER TABLE [dbo].[CustomerCashSavingsAccountAssociates] CHECK CONSTRAINT [FK_CustomerCashSavingsAccountAssociates_CustomerAssociates]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsAccountAssociates_CustomerCashSavingsAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsAccountAssociates_CustomerCashSavingsAccount] FOREIGN KEY([CCSA_AccountId])
REFERENCES [dbo].[CustomerCashSavingsAccount] ([CCSA_AccountId])
GO
ALTER TABLE [dbo].[CustomerCashSavingsAccountAssociates] CHECK CONSTRAINT [FK_CustomerCashSavingsAccountAssociates_CustomerCashSavingsAccount]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsNetPosition_CustomerCashSavingsAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_CustomerCashSavingsAccount] FOREIGN KEY([CCSA_AccountId])
REFERENCES [dbo].[CustomerCashSavingsAccount] ([CCSA_AccountId])
GO
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_CustomerCashSavingsAccount]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsNetPosition_CustomerCashSavingsAccount1]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_CustomerCashSavingsAccount1] FOREIGN KEY([CCSA_AccountId])
REFERENCES [dbo].[CustomerCashSavingsAccount] ([CCSA_AccountId])
GO
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_CustomerCashSavingsAccount1]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsNetPosition_XMLDebtIssuer]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLDebtIssuer] FOREIGN KEY([XDI_DebtIssuerCode])
REFERENCES [dbo].[XMLDebtIssuer] ([XDI_DebtIssuerCode])
GO
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLDebtIssuer]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsNetPosition_XMLFrequency]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLFrequency] FOREIGN KEY([XF_InterestPayoutFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLFrequency]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsNetPosition_XMLFrequency1]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLFrequency1] FOREIGN KEY([XF_CompoundInterestFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLFrequency1]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsNetPosition_XMLInterestBasis]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLInterestBasis] FOREIGN KEY([XIB_InterestBasisCode])
REFERENCES [dbo].[XMLInterestBasis] ([XIB_InterestBasisCode])
GO
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLInterestBasis]
GO
/****** Object:  ForeignKey [FK_CustomerCashSavingsPortfolio_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsPortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsPortfolio_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerCollectibleNetPosition_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCollectibleNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCollectibleNetPosition_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerCollectibleNetPosition] CHECK CONSTRAINT [FK_CustomerCollectibleNetPosition_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerInvestmentCollectiblePortfolio_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerCollectibleNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentCollectiblePortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerCollectibleNetPosition] CHECK CONSTRAINT [FK_CustomerInvestmentCollectiblePortfolio_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerEquityBrokerage_Customer]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityBrokerage]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerEquityBrokerage_Customer] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO
ALTER TABLE [dbo].[CustomerEquityBrokerage] CHECK CONSTRAINT [FK_CustomerEquityBrokerage_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerEquityBrokerage_XMLBroker]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityBrokerage]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityBrokerage_XMLBroker] FOREIGN KEY([XB_BrokerCode])
REFERENCES [dbo].[XMLBroker] ([XB_BrokerCode])
GO
ALTER TABLE [dbo].[CustomerEquityBrokerage] CHECK CONSTRAINT [FK_CustomerEquityBrokerage_XMLBroker]
GO
/****** Object:  ForeignKey [FK_CustomerEquityDematAccount_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityDematAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityDematAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerEquityDematAccount] CHECK CONSTRAINT [FK_CustomerEquityDematAccount_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerEquityDematAccount_XMLModeOfHolding]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityDematAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityDematAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO
ALTER TABLE [dbo].[CustomerEquityDematAccount] CHECK CONSTRAINT [FK_CustomerEquityDematAccount_XMLModeOfHolding]
GO
/****** Object:  ForeignKey [FK_CustomerEquityDematAccountAssociates_CustomerAssociates]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityDematAccountAssociates]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerEquityDematAccountAssociates_CustomerAssociates] FOREIGN KEY([CAS_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO
ALTER TABLE [dbo].[CustomerEquityDematAccountAssociates] CHECK CONSTRAINT [FK_CustomerEquityDematAccountAssociates_CustomerAssociates]
GO
/****** Object:  ForeignKey [FK_CustomerEquityDematAccountAssociates_CustomerEquityDematAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityDematAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityDematAccountAssociates_CustomerEquityDematAccount] FOREIGN KEY([CEDA_DematAccountId])
REFERENCES [dbo].[CustomerEquityDematAccount] ([CEDA_DematAccountId])
GO
ALTER TABLE [dbo].[CustomerEquityDematAccountAssociates] CHECK CONSTRAINT [FK_CustomerEquityDematAccountAssociates_CustomerEquityDematAccount]
GO
/****** Object:  ForeignKey [FK_CustomerEquityNetPosition_CustomerEquityTradeAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityNetPosition_CustomerEquityTradeAccount] FOREIGN KEY([CETA_AccountId])
REFERENCES [dbo].[CustomerEquityTradeAccount] ([CETA_AccountId])
GO
ALTER TABLE [dbo].[CustomerEquityNetPosition] CHECK CONSTRAINT [FK_CustomerEquityNetPosition_CustomerEquityTradeAccount]
GO
/****** Object:  ForeignKey [FK_CustomerEquityNetPosition_ProductEquityMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityNetPosition_ProductEquityMaster] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO
ALTER TABLE [dbo].[CustomerEquityNetPosition] CHECK CONSTRAINT [FK_CustomerEquityNetPosition_ProductEquityMaster]
GO
/****** Object:  ForeignKey [FK_CustomerEquityOdinBSEXtrnlTransaction_CustomerEquityTransaction]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityOdinBSEXtrnlTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityOdinBSEXtrnlTransaction_CustomerEquityTransaction] FOREIGN KEY([CET_EqTransId])
REFERENCES [dbo].[CustomerEquityTransaction] ([CET_EqTransId])
GO
ALTER TABLE [dbo].[CustomerEquityOdinBSEXtrnlTransaction] CHECK CONSTRAINT [FK_CustomerEquityOdinBSEXtrnlTransaction_CustomerEquityTransaction]
GO
/****** Object:  ForeignKey [FK_CustomerEquityOdinNSEXtrnlTransaction_CustomerEquityTransaction]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityOdinNSEXtrnlTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityOdinNSEXtrnlTransaction_CustomerEquityTransaction] FOREIGN KEY([CET_EqTransId])
REFERENCES [dbo].[CustomerEquityTransaction] ([CET_EqTransId])
GO
ALTER TABLE [dbo].[CustomerEquityOdinNSEXtrnlTransaction] CHECK CONSTRAINT [FK_CustomerEquityOdinNSEXtrnlTransaction_CustomerEquityTransaction]
GO
/****** Object:  ForeignKey [FK_CustomerAssetGroupEquityAccount_ProductAssetGroup]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTradeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAssetGroupEquityAccount_ProductAssetGroup] FOREIGN KEY([PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetGroup] ([PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerEquityTradeAccount] CHECK CONSTRAINT [FK_CustomerAssetGroupEquityAccount_ProductAssetGroup]
GO
/****** Object:  ForeignKey [FK_CustomerEquityTradeAccount_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTradeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTradeAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerEquityTradeAccount] CHECK CONSTRAINT [FK_CustomerEquityTradeAccount_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerEquityTradeAccount_XMLBroker]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTradeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTradeAccount_XMLBroker] FOREIGN KEY([XB_BrokerCode])
REFERENCES [dbo].[XMLBroker] ([XB_BrokerCode])
GO
ALTER TABLE [dbo].[CustomerEquityTradeAccount] CHECK CONSTRAINT [FK_CustomerEquityTradeAccount_XMLBroker]
GO
/****** Object:  ForeignKey [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityDematAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityDematAccount] FOREIGN KEY([CEDA_DematAccountId])
REFERENCES [dbo].[CustomerEquityDematAccount] ([CEDA_DematAccountId])
GO
ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation] CHECK CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityDematAccount]
GO
/****** Object:  ForeignKey [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityTradeAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityTradeAccount] FOREIGN KEY([CETA_AccountId])
REFERENCES [dbo].[CustomerEquityTradeAccount] ([CETA_AccountId])
GO
ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation] CHECK CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityTradeAccount]
GO
/****** Object:  ForeignKey [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityTradeDematAccountAssociation]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityTradeDematAccountAssociation] FOREIGN KEY([CETDAA_AssociationId])
REFERENCES [dbo].[CustomerEquityTradeDematAccountAssociation] ([CETDAA_AssociationId])
GO
ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation] CHECK CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityTradeDematAccountAssociation]
GO
/****** Object:  ForeignKey [FK_CustomerEquityTransaction_CustomerEquityTradeAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_CustomerEquityTradeAccount] FOREIGN KEY([CETA_AccountId])
REFERENCES [dbo].[CustomerEquityTradeAccount] ([CETA_AccountId])
GO
ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_CustomerEquityTradeAccount]
GO
/****** Object:  ForeignKey [FK_CustomerEquityTransaction_ProductEquity]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_ProductEquity] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO
ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_ProductEquity]
GO
/****** Object:  ForeignKey [FK_CustomerEquityTransaction_ProductEquityMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_ProductEquityMaster] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO
ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_ProductEquityMaster]
GO
/****** Object:  ForeignKey [FK_CustomerEquityTransaction_XMLBroker]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_XMLBroker] FOREIGN KEY([XB_BrokerCode])
REFERENCES [dbo].[XMLBroker] ([XB_BrokerCode])
GO
ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_XMLBroker]
GO
/****** Object:  ForeignKey [FK_CustomerEquityTransaction_XMLExchange]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_XMLExchange] FOREIGN KEY([XE_ExchangeCode])
REFERENCES [dbo].[XMLExchange] ([XE_ExchangeCode])
GO
ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_XMLExchange]
GO
/****** Object:  ForeignKey [FK_CustomerEquityTransaction_XMLExternalSource]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_XMLExternalSource] FOREIGN KEY([XES_SourceCode])
REFERENCES [dbo].[XMLExternalSource] ([XES_SourceCode])
GO
ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_XMLExternalSource]
GO
/****** Object:  ForeignKey [FK_CustomerInvestmentEquityTransaction_WerpEquityTransactionType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentEquityTransaction_WerpEquityTransactionType] FOREIGN KEY([WETT_TransactionCode])
REFERENCES [dbo].[WerpEquityTransactionType] ([WETT_TransactionCode])
GO
ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerInvestmentEquityTransaction_WerpEquityTransactionType]
GO
/****** Object:  ForeignKey [FK_CustomerFixedIncomeAcccountAssociates_CustomerAssociates]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeAcccountAssociates]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeAcccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociateId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeAcccountAssociates] CHECK CONSTRAINT [FK_CustomerFixedIncomeAcccountAssociates_CustomerAssociates]
GO
/****** Object:  ForeignKey [FK_CustomerFixedIncomeAcccountAssociates_CustomerFixedIncomeAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeAcccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeAcccountAssociates_CustomerFixedIncomeAccount] FOREIGN KEY([CFIA_AccountId])
REFERENCES [dbo].[CustomerFixedIncomeAccount] ([CFIA_AccountId])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeAcccountAssociates] CHECK CONSTRAINT [FK_CustomerFixedIncomeAcccountAssociates_CustomerFixedIncomeAccount]
GO
/****** Object:  ForeignKey [FK_CustomerFixedIncomeAccount_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeAccount] CHECK CONSTRAINT [FK_CustomerFixedIncomeAccount_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerFixedIncomeAccount_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeAccount_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeAccount] CHECK CONSTRAINT [FK_CustomerFixedIncomeAccount_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerFixedIncomeAccount_XMLModeOfHolding]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeAccount] CHECK CONSTRAINT [FK_CustomerFixedIncomeAccount_XMLModeOfHolding]
GO
/****** Object:  ForeignKey [FK_CustomerFixedIncomeNetPosition_CustomerFixedIncomeAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_CustomerFixedIncomeAccount] FOREIGN KEY([CFIA_AccountId])
REFERENCES [dbo].[CustomerFixedIncomeAccount] ([CFIA_AccountId])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_CustomerFixedIncomeAccount]
GO
/****** Object:  ForeignKey [FK_CustomerFixedIncomeNetPosition_XMLDebtIssuer]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLDebtIssuer] FOREIGN KEY([XDI_DebtIssuerCode])
REFERENCES [dbo].[XMLDebtIssuer] ([XDI_DebtIssuerCode])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLDebtIssuer]
GO
/****** Object:  ForeignKey [FK_CustomerFixedIncomeNetPosition_XMLFrequency]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency] FOREIGN KEY([XF_CompoundInterestFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency]
GO
/****** Object:  ForeignKey [FK_CustomerFixedIncomeNetPosition_XMLFrequency1]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency1] FOREIGN KEY([XF_DepositFrquencycode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency1]
GO
/****** Object:  ForeignKey [FK_CustomerFixedIncomeNetPosition_XMLFrequency2]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency2] FOREIGN KEY([XF_InterestPayableFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency2]
GO
/****** Object:  ForeignKey [FK_CustomerFixedIncomeNetPosition_XMLInterestBasis]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLInterestBasis] FOREIGN KEY([XIB_InterestBasisCode])
REFERENCES [dbo].[XMLInterestBasis] ([XIB_InterestBasisCode])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLInterestBasis]
GO
/****** Object:  ForeignKey [FK_CustomerInvestmentFixedIncomePortfolio_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentFixedIncomePortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerInvestmentFixedIncomePortfolio_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerGoals_CustomerMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGoal]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerGoals_CustomerMaster] FOREIGN KEY([CM_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO
ALTER TABLE [dbo].[CustomerGoal] CHECK CONSTRAINT [FK_CustomerGoals_CustomerMaster]
GO
/****** Object:  ForeignKey [FK_CustomerGoldNetPosition_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGoldNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGoldNetPosition_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerGoldNetPosition] CHECK CONSTRAINT [FK_CustomerGoldNetPosition_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerGoldNetPosition_XMLMeasureCode]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGoldNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGoldNetPosition_XMLMeasureCode] FOREIGN KEY([XMC_MeasureCode])
REFERENCES [dbo].[XMLMeasureCode] ([XMC_MeasureCode])
GO
ALTER TABLE [dbo].[CustomerGoldNetPosition] CHECK CONSTRAINT [FK_CustomerGoldNetPosition_XMLMeasureCode]
GO
/****** Object:  ForeignKey [FK_CustomerInvestmentGoldPortfolio_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGoldNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentGoldPortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerGoldNetPosition] CHECK CONSTRAINT [FK_CustomerInvestmentGoldPortfolio_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingAccount_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerGovtSavingAccount] CHECK CONSTRAINT [FK_CustomerGovtSavingAccount_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingAccount_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccount_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerGovtSavingAccount] CHECK CONSTRAINT [FK_CustomerGovtSavingAccount_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingAccount_ProductAssetInstrumentCategory1]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccount_ProductAssetInstrumentCategory1] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerGovtSavingAccount] CHECK CONSTRAINT [FK_CustomerGovtSavingAccount_ProductAssetInstrumentCategory1]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingAccount_XMLModeOfHolding]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO
ALTER TABLE [dbo].[CustomerGovtSavingAccount] CHECK CONSTRAINT [FK_CustomerGovtSavingAccount_XMLModeOfHolding]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingAccountAssociate_CustomerAssociates]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccountAssociate_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO
ALTER TABLE [dbo].[CustomerGovtSavingAccountAssociates] CHECK CONSTRAINT [FK_CustomerGovtSavingAccountAssociate_CustomerAssociates]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingAccountAssociate_CustomerGovtSavingAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccountAssociate_CustomerGovtSavingAccount] FOREIGN KEY([CGSA_AccountId])
REFERENCES [dbo].[CustomerGovtSavingAccount] ([CGSA_AccountId])
GO
ALTER TABLE [dbo].[CustomerGovtSavingAccountAssociates] CHECK CONSTRAINT [FK_CustomerGovtSavingAccountAssociate_CustomerGovtSavingAccount]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingNetPosition_CustomerGovtSavingAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_CustomerGovtSavingAccount] FOREIGN KEY([CGSA_AccountId])
REFERENCES [dbo].[CustomerGovtSavingAccount] ([CGSA_AccountId])
GO
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_CustomerGovtSavingAccount]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingNetPosition_XMLDebtIssuer]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLDebtIssuer] FOREIGN KEY([XDI_DebtIssuerCode])
REFERENCES [dbo].[XMLDebtIssuer] ([XDI_DebtIssuerCode])
GO
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLDebtIssuer]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingNetPosition_XMLFrequency]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency] FOREIGN KEY([XF_CompoundInterestFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingNetPosition_XMLFrequency1]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency1] FOREIGN KEY([XF_DepositFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency1]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingNetPosition_XMLFrequency2]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency2] FOREIGN KEY([XF_InterestPayableFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency2]
GO
/****** Object:  ForeignKey [FK_CustomerGovtSavingNetPosition_XMLInterestBasis]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLInterestBasis] FOREIGN KEY([XIB_InterestBasisCode])
REFERENCES [dbo].[XMLInterestBasis] ([XIB_InterestBasisCode])
GO
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLInterestBasis]
GO
/****** Object:  ForeignKey [FK_CustomerInvestmentGovtSavingPortfolio_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentGovtSavingPortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerInvestmentGovtSavingPortfolio_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerIncome_CustomerMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerIncome]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerIncome_CustomerMaster] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO
ALTER TABLE [dbo].[CustomerIncome] CHECK CONSTRAINT [FK_CustomerIncome_CustomerMaster]
GO
/****** Object:  ForeignKey [FK_CustomerInsuranceAccount_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerInsuranceAccount] CHECK CONSTRAINT [FK_CustomerInsuranceAccount_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerInsuranceAccount_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceAccount_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerInsuranceAccount] CHECK CONSTRAINT [FK_CustomerInsuranceAccount_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerInsuranceAccountAssociates_CustomerAssociates]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceAccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO
ALTER TABLE [dbo].[CustomerInsuranceAccountAssociates] CHECK CONSTRAINT [FK_CustomerInsuranceAccountAssociates_CustomerAssociates]
GO
/****** Object:  ForeignKey [FK_CustomerInsuranceAccountAssociates_CustomerInsuranceAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceAccountAssociates_CustomerInsuranceAccount] FOREIGN KEY([CIA_AccountId])
REFERENCES [dbo].[CustomerInsuranceAccount] ([CIA_AccountId])
GO
ALTER TABLE [dbo].[CustomerInsuranceAccountAssociates] CHECK CONSTRAINT [FK_CustomerInsuranceAccountAssociates_CustomerInsuranceAccount]
GO
/****** Object:  ForeignKey [FK_CustomerInsuranceMoneyBackEpisodes_CustomerInsuranceNetPosition]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceMoneyBackEpisodes]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceMoneyBackEpisodes_CustomerInsuranceNetPosition] FOREIGN KEY([CINP_InsuranceNPId])
REFERENCES [dbo].[CustomerInsuranceNetPosition] ([CINP_InsuranceNPId])
GO
ALTER TABLE [dbo].[CustomerInsuranceMoneyBackEpisodes] CHECK CONSTRAINT [FK_CustomerInsuranceMoneyBackEpisodes_CustomerInsuranceNetPosition]
GO
/****** Object:  ForeignKey [FK_CustomerInsuranceNetPosition_CustomerInsuranceAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceNetPosition_CustomerInsuranceAccount] FOREIGN KEY([CIA_AccountId])
REFERENCES [dbo].[CustomerInsuranceAccount] ([CIA_AccountId])
GO
ALTER TABLE [dbo].[CustomerInsuranceNetPosition] CHECK CONSTRAINT [FK_CustomerInsuranceNetPosition_CustomerInsuranceAccount]
GO
/****** Object:  ForeignKey [FK_CustomerInsuranceNetPosition_XMLFrequency]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceNetPosition_XMLFrequency] FOREIGN KEY([XF_PremiumFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerInsuranceNetPosition] CHECK CONSTRAINT [FK_CustomerInsuranceNetPosition_XMLFrequency]
GO
/****** Object:  ForeignKey [FK_CustomerInsuranceNetPosition_XMLInsuranceIssuer]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceNetPosition_XMLInsuranceIssuer] FOREIGN KEY([XII_InsuranceIssuerCode])
REFERENCES [dbo].[XMLInsuranceIssuer] ([XII_InsuranceIssuerCode])
GO
ALTER TABLE [dbo].[CustomerInsuranceNetPosition] CHECK CONSTRAINT [FK_CustomerInsuranceNetPosition_XMLInsuranceIssuer]
GO
/****** Object:  ForeignKey [FK_CustomerInsurancePortfolio_CustomerInsurancePortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsurancePortfolio_CustomerInsurancePortfolio] FOREIGN KEY([CINP_InsuranceNPId])
REFERENCES [dbo].[CustomerInsuranceNetPosition] ([CINP_InsuranceNPId])
GO
ALTER TABLE [dbo].[CustomerInsuranceNetPosition] CHECK CONSTRAINT [FK_CustomerInsurancePortfolio_CustomerInsurancePortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerInsurancePortfolio_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsurancePortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerInsuranceNetPosition] CHECK CONSTRAINT [FK_CustomerInsurancePortfolio_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerInsurabceULIPPlan_CustomerInsurancePortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceULIPPlan]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsurabceULIPPlan_CustomerInsurancePortfolio] FOREIGN KEY([CINP_InsuranceNPId])
REFERENCES [dbo].[CustomerInsuranceNetPosition] ([CINP_InsuranceNPId])
GO
ALTER TABLE [dbo].[CustomerInsuranceULIPPlan] CHECK CONSTRAINT [FK_CustomerInsurabceULIPPlan_CustomerInsurancePortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerInsurabceULIPPlan_WerpULIPSubPlan]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerInsuranceULIPPlan]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsurabceULIPPlan_WerpULIPSubPlan] FOREIGN KEY([WUSP_ULIPSubPlanCode])
REFERENCES [dbo].[WerpULIPSubPlan] ([WUSP_ULIPSubPlanCode])
GO
ALTER TABLE [dbo].[CustomerInsuranceULIPPlan] CHECK CONSTRAINT [FK_CustomerInsurabceULIPPlan_WerpULIPSubPlan]
GO
/****** Object:  ForeignKey [FK_CustomerMFCAMSXtrnlProfile_Customer]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMFCAMSXtrnlProfile]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMFCAMSXtrnlProfile_Customer] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO
ALTER TABLE [dbo].[CustomerMFCAMSXtrnlProfile] CHECK CONSTRAINT [FK_CustomerMFCAMSXtrnlProfile_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerMFCAMSXtrnlSystematicSetup_CustomerMutualFundSystematicSetup]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMFCAMSXtrnlSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMFCAMSXtrnlSystematicSetup_CustomerMutualFundSystematicSetup] FOREIGN KEY([CMFSS_SystematicSetupId])
REFERENCES [dbo].[CustomerMutualFundSystematicSetup] ([CMFSS_SystematicSetupId])
GO
ALTER TABLE [dbo].[CustomerMFCAMSXtrnlSystematicSetup] CHECK CONSTRAINT [FK_CustomerMFCAMSXtrnlSystematicSetup_CustomerMutualFundSystematicSetup]
GO
/****** Object:  ForeignKey [FK_CustomerCAMSMFTransaction_CustomerMFTransaction]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMFCAMSXtrnlTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCAMSMFTransaction_CustomerMFTransaction] FOREIGN KEY([CMFT_MFTransId])
REFERENCES [dbo].[CustomerMutualFundTransaction] ([CMFT_MFTransId])
GO
ALTER TABLE [dbo].[CustomerMFCAMSXtrnlTransaction] CHECK CONSTRAINT [FK_CustomerCAMSMFTransaction_CustomerMFTransaction]
GO
/****** Object:  ForeignKey [FK_CustomerMFKarvyXtrnlProfile_Customer]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMFKarvyXtrnlProfile]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMFKarvyXtrnlProfile_Customer] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO
ALTER TABLE [dbo].[CustomerMFKarvyXtrnlProfile] CHECK CONSTRAINT [FK_CustomerMFKarvyXtrnlProfile_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerMFTempletonXtrnlSystematicSetup_CustomerMutualFundSystematicSetup]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMFKarvyXtrnlSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMFTempletonXtrnlSystematicSetup_CustomerMutualFundSystematicSetup] FOREIGN KEY([CMFSS_SystematicSetupId])
REFERENCES [dbo].[CustomerMutualFundSystematicSetup] ([CMFSS_SystematicSetupId])
GO
ALTER TABLE [dbo].[CustomerMFKarvyXtrnlSystematicSetup] CHECK CONSTRAINT [FK_CustomerMFTempletonXtrnlSystematicSetup_CustomerMutualFundSystematicSetup]
GO
/****** Object:  ForeignKey [FK_CustomerMFKarvyXtrnlTransaction_CustomerMutualFundTransaction]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMFKarvyXtrnlTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMFKarvyXtrnlTransaction_CustomerMutualFundTransaction] FOREIGN KEY([CMFT_MFTransId])
REFERENCES [dbo].[CustomerMutualFundTransaction] ([CMFT_MFTransId])
GO
ALTER TABLE [dbo].[CustomerMFKarvyXtrnlTransaction] CHECK CONSTRAINT [FK_CustomerMFKarvyXtrnlTransaction_CustomerMutualFundTransaction]
GO
/****** Object:  ForeignKey [FK_CustomerAssetGroupMutualFundAccount_ProductAssetGroup]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAssetGroupMutualFundAccount_ProductAssetGroup] FOREIGN KEY([PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetGroup] ([PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundAccount] CHECK CONSTRAINT [FK_CustomerAssetGroupMutualFundAccount_ProductAssetGroup]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundAccount_CustomerMutualFundAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccount_CustomerMutualFundAccount] FOREIGN KEY([PA_AMCCode])
REFERENCES [dbo].[ProductAMC] ([PA_AMCCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundAccount] CHECK CONSTRAINT [FK_CustomerMutualFundAccount_CustomerMutualFundAccount]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundAccount_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerMutualFundAccount] CHECK CONSTRAINT [FK_CustomerMutualFundAccount_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundAccount_XMLModeOfHolding]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundAccount] CHECK CONSTRAINT [FK_CustomerMutualFundAccount_XMLModeOfHolding]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundAccountAssociates_CustomerAssociates]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO
ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates] CHECK CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerAssociates]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundAccountAssociates_CustomerMutualFundAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerMutualFundAccount] FOREIGN KEY([CMFA_AccountId])
REFERENCES [dbo].[CustomerMutualFundAccount] ([CMFA_AccountId])
GO
ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates] CHECK CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerMutualFundAccount]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundAccountAssociates_CustomerMutualFundAccountAssociates]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerMutualFundAccountAssociates] FOREIGN KEY([CMFAA_AccountAssociationId])
REFERENCES [dbo].[CustomerMutualFundAccountAssociates] ([CMFAA_AccountAssociationId])
GO
ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates] CHECK CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerMutualFundAccountAssociates]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundNetPosition_CustomerMutualFundAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundNetPosition_CustomerMutualFundAccount] FOREIGN KEY([CMFA_AccountId])
REFERENCES [dbo].[CustomerMutualFundAccount] ([CMFA_AccountId])
GO
ALTER TABLE [dbo].[CustomerMutualFundNetPosition] CHECK CONSTRAINT [FK_CustomerMutualFundNetPosition_CustomerMutualFundAccount]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundNetPosition_ProductAMCSchemePlan]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundNetPosition_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundNetPosition] CHECK CONSTRAINT [FK_CustomerMutualFundNetPosition_ProductAMCSchemePlan]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundSystematicSetup_CustomerMutualFundAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_CustomerMutualFundAccount] FOREIGN KEY([CMFA_AccountId])
REFERENCES [dbo].[CustomerMutualFundAccount] ([CMFA_AccountId])
GO
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_CustomerMutualFundAccount]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundSystematicSetup_ProductAMCSchemePlan]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_ProductAMCSchemePlan]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundSystematicSetup_XMLExternalSource]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLExternalSource] FOREIGN KEY([XES_SourceCode])
REFERENCES [dbo].[XMLExternalSource] ([XES_SourceCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLExternalSource]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundSystematicSetup_XMLFrequency]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLFrequency] FOREIGN KEY([XF_FrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLFrequency]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundSystematicSetup_XMLPaymentMode]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLPaymentMode] FOREIGN KEY([XPM_PaymentModeCode])
REFERENCES [dbo].[XMLPaymentMode] ([XPM_PaymentModeCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLPaymentMode]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundSystematicSetup_XMLSystematicTransactionType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLSystematicTransactionType] FOREIGN KEY([XSTT_SystematicTypeCode])
REFERENCES [dbo].[XMLSystematicTransactionType] ([XSTT_SystematicTypeCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLSystematicTransactionType]
GO
/****** Object:  ForeignKey [FK_CustomerMFTransaction_ProductAMCSchemePlan]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerMFTransaction_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundTransaction] CHECK CONSTRAINT [FK_CustomerMFTransaction_ProductAMCSchemePlan]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundTransaction_CustomerMutualFundAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerMutualFundTransaction_CustomerMutualFundAccount] FOREIGN KEY([CMFA_AccountId])
REFERENCES [dbo].[CustomerMutualFundAccount] ([CMFA_AccountId])
GO
ALTER TABLE [dbo].[CustomerMutualFundTransaction] CHECK CONSTRAINT [FK_CustomerMutualFundTransaction_CustomerMutualFundAccount]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundTransaction_MutualFundTransactionType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerMutualFundTransaction_MutualFundTransactionType] FOREIGN KEY([WMTT_TransactionClassificationCode])
REFERENCES [dbo].[WerpMutualFundTransactionType] ([WMTT_TransactionClassificationCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundTransaction] CHECK CONSTRAINT [FK_CustomerMutualFundTransaction_MutualFundTransactionType]
GO
/****** Object:  ForeignKey [FK_CustomerMutualFundTransaction_XMLExternalSource]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerMutualFundTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerMutualFundTransaction_XMLExternalSource] FOREIGN KEY([XES_SourceCode])
REFERENCES [dbo].[XMLExternalSource] ([XES_SourceCode])
GO
ALTER TABLE [dbo].[CustomerMutualFundTransaction] CHECK CONSTRAINT [FK_CustomerMutualFundTransaction_XMLExternalSource]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGratuitiesAccount_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGratuitiesAccount_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGratuitiesAccount_XMLModeOfHolding]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO
ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_XMLModeOfHolding]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGratuitiesNetPosition_CustomerPensionandGratuitiesAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_CustomerPensionandGratuitiesAccount] FOREIGN KEY([CPGA_AccountId])
REFERENCES [dbo].[CustomerPensionandGratuitiesAccount] ([CPGA_AccountId])
GO
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_CustomerPensionandGratuitiesAccount]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGratuitiesNetPosition_XMLDebtIssuer]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLDebtIssuer] FOREIGN KEY([XDI_DebtIssuerCode])
REFERENCES [dbo].[XMLDebtIssuer] ([XDI_DebtIssuerCode])
GO
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLDebtIssuer]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGratuitiesNetPosition_XMLFiscalYear]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFiscalYear] FOREIGN KEY([XFY_FiscalYearCode])
REFERENCES [dbo].[XMLFiscalYear] ([XFY_FiscalYearCode])
GO
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFiscalYear]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGratuitiesNetPosition_XMLFrequency]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFrequency] FOREIGN KEY([XF_CompoundInterestFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFrequency]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGratuitiesNetPosition_XMLFrequency1]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFrequency1] FOREIGN KEY([XF_InterestPayableFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFrequency1]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGratuitiesNetPosition_XMLInterestBasis]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLInterestBasis] FOREIGN KEY([XIB_InterestBasisCode])
REFERENCES [dbo].[XMLInterestBasis] ([XIB_InterestBasisCode])
GO
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLInterestBasis]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGratuitiesPortfolio_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesPortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesPortfolio_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGrauitiesAccountAssociates_CustomerAssociates]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGrauitiesAccountAssociates]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerPensionandGrauitiesAccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO
ALTER TABLE [dbo].[CustomerPensionandGrauitiesAccountAssociates] CHECK CONSTRAINT [FK_CustomerPensionandGrauitiesAccountAssociates_CustomerAssociates]
GO
/****** Object:  ForeignKey [FK_CustomerPensionandGrauitiesAccountAssociates_CustomerPensionandGratuitiesAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPensionandGrauitiesAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGrauitiesAccountAssociates_CustomerPensionandGratuitiesAccount] FOREIGN KEY([CPGA_AccountId])
REFERENCES [dbo].[CustomerPensionandGratuitiesAccount] ([CPGA_AccountId])
GO
ALTER TABLE [dbo].[CustomerPensionandGrauitiesAccountAssociates] CHECK CONSTRAINT [FK_CustomerPensionandGrauitiesAccountAssociates_CustomerPensionandGratuitiesAccount]
GO
/****** Object:  ForeignKey [FK_CustomerPersonalNetPosition_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPersonalNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPersonalNetPosition_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerPersonalNetPosition] CHECK CONSTRAINT [FK_CustomerPersonalNetPosition_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerPersonalPortfolio_ProductAssetInstrumentSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPersonalNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPersonalPortfolio_ProductAssetInstrumentSubCategory] FOREIGN KEY([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubCategory] ([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerPersonalNetPosition] CHECK CONSTRAINT [FK_CustomerPersonalPortfolio_ProductAssetInstrumentSubCategory]
GO
/****** Object:  ForeignKey [FK_CustomerPortfolio_Customer]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPortfolio]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPortfolio_Customer] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO
ALTER TABLE [dbo].[CustomerPortfolio] CHECK CONSTRAINT [FK_CustomerPortfolio_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerProof_Customer]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerProof]  WITH CHECK ADD  CONSTRAINT [FK_CustomerProof_Customer] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO
ALTER TABLE [dbo].[CustomerProof] CHECK CONSTRAINT [FK_CustomerProof_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerProof_XMLProof]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerProof]  WITH CHECK ADD  CONSTRAINT [FK_CustomerProof_XMLProof] FOREIGN KEY([XP_ProofCode])
REFERENCES [dbo].[XMLProof] ([XP_ProofCode])
GO
ALTER TABLE [dbo].[CustomerProof] CHECK CONSTRAINT [FK_CustomerProof_XMLProof]
GO
/****** Object:  ForeignKey [FK_CustomerPropertyAccount_CustomerPortfolio]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPropertyAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO
ALTER TABLE [dbo].[CustomerPropertyAccount] CHECK CONSTRAINT [FK_CustomerPropertyAccount_CustomerPortfolio]
GO
/****** Object:  ForeignKey [FK_CustomerPropertyAccount_ProductAssetInstrumentSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPropertyAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyAccount_ProductAssetInstrumentSubCategory] FOREIGN KEY([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubCategory] ([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerPropertyAccount] CHECK CONSTRAINT [FK_CustomerPropertyAccount_ProductAssetInstrumentSubCategory]
GO
/****** Object:  ForeignKey [FK_CustomerPropertyAccount_XMLModeOfHolding]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPropertyAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO
ALTER TABLE [dbo].[CustomerPropertyAccount] CHECK CONSTRAINT [FK_CustomerPropertyAccount_XMLModeOfHolding]
GO
/****** Object:  ForeignKey [FK_CustomerPropertyAccountAssociates_CustomerAssociates]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPropertyAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyAccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO
ALTER TABLE [dbo].[CustomerPropertyAccountAssociates] CHECK CONSTRAINT [FK_CustomerPropertyAccountAssociates_CustomerAssociates]
GO
/****** Object:  ForeignKey [FK_CustomerPropertyAccountAssociates_CustomerPropertyAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPropertyAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyAccountAssociates_CustomerPropertyAccount] FOREIGN KEY([CPA_AccountId])
REFERENCES [dbo].[CustomerPropertyAccount] ([CPA_AccountId])
GO
ALTER TABLE [dbo].[CustomerPropertyAccountAssociates] CHECK CONSTRAINT [FK_CustomerPropertyAccountAssociates_CustomerPropertyAccount]
GO
/****** Object:  ForeignKey [FK_CustomerInvestmentPropertyPortfolio_ProductAssetInstrumentSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPropertyNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentPropertyPortfolio_ProductAssetInstrumentSubCategory] FOREIGN KEY([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubCategory] ([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[CustomerPropertyNetPosition] CHECK CONSTRAINT [FK_CustomerInvestmentPropertyPortfolio_ProductAssetInstrumentSubCategory]
GO
/****** Object:  ForeignKey [FK_CustomerPropertyNetPosition_CustomerPropertyAccount]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPropertyNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyNetPosition_CustomerPropertyAccount] FOREIGN KEY([CPA_AccountId])
REFERENCES [dbo].[CustomerPropertyAccount] ([CPA_AccountId])
GO
ALTER TABLE [dbo].[CustomerPropertyNetPosition] CHECK CONSTRAINT [FK_CustomerPropertyNetPosition_CustomerPropertyAccount]
GO
/****** Object:  ForeignKey [FK_CustomerPropertyNetPosition_XMLMeasureCode]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerPropertyNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyNetPosition_XMLMeasureCode] FOREIGN KEY([XMC_MeasureCode])
REFERENCES [dbo].[XMLMeasureCode] ([XMC_MeasureCode])
GO
ALTER TABLE [dbo].[CustomerPropertyNetPosition] CHECK CONSTRAINT [FK_CustomerPropertyNetPosition_XMLMeasureCode]
GO
/****** Object:  ForeignKey [FK_CustomerRiskProfile_CustomerMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerRiskProfile]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerRiskProfile_CustomerMaster] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO
ALTER TABLE [dbo].[CustomerRiskProfile] CHECK CONSTRAINT [FK_CustomerRiskProfile_CustomerMaster]
GO
/****** Object:  ForeignKey [FK_CustomerRiskProfile_QuestionChoice]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerRiskProfile]  WITH CHECK ADD  CONSTRAINT [FK_CustomerRiskProfile_QuestionChoice] FOREIGN KEY([QCH_ChoiceId])
REFERENCES [dbo].[WerpQuestionChoice] ([WQCH_ChoiceId])
GO
ALTER TABLE [dbo].[CustomerRiskProfile] CHECK CONSTRAINT [FK_CustomerRiskProfile_QuestionChoice]
GO
/****** Object:  ForeignKey [FK_CustomerRiskProfile_QuestionMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[CustomerRiskProfile]  WITH CHECK ADD  CONSTRAINT [FK_CustomerRiskProfile_QuestionMaster] FOREIGN KEY([QM_QuestionId])
REFERENCES [dbo].[WerpQuestionMaster] ([WQM_QuestionId])
GO
ALTER TABLE [dbo].[CustomerRiskProfile] CHECK CONSTRAINT [FK_CustomerRiskProfile_QuestionMaster]
GO
/****** Object:  ForeignKey [FK_ProductAMCSchemeMapping_ProductAMCSchemePlan]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductAMCSchemeMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemeMapping_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO
ALTER TABLE [dbo].[ProductAMCSchemeMapping] CHECK CONSTRAINT [FK_ProductAMCSchemeMapping_ProductAMCSchemePlan]
GO
/****** Object:  ForeignKey [FK_ProductAMCSchemeMapping_ProductAMCSchemePlan1]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductAMCSchemeMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemeMapping_ProductAMCSchemePlan1] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO
ALTER TABLE [dbo].[ProductAMCSchemeMapping] CHECK CONSTRAINT [FK_ProductAMCSchemeMapping_ProductAMCSchemePlan1]
GO
/****** Object:  ForeignKey [FK_ProductAMCSchemePlan_ProductAMC]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductAMCSchemePlan]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemePlan_ProductAMC] FOREIGN KEY([PA_AMCCode])
REFERENCES [dbo].[ProductAMC] ([PA_AMCCode])
GO
ALTER TABLE [dbo].[ProductAMCSchemePlan] CHECK CONSTRAINT [FK_ProductAMCSchemePlan_ProductAMC]
GO
/****** Object:  ForeignKey [FK_ProductAMCSchemePlan_ProductAssetInstrumentSubSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductAMCSchemePlan]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemePlan_ProductAssetInstrumentSubSubCategory] FOREIGN KEY([PAISSC_AssetInstrumentSubSubCategoryCode], [PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubSubCategory] ([PAISSC_AssetInstrumentSubSubCategoryCode], [PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[ProductAMCSchemePlan] CHECK CONSTRAINT [FK_ProductAMCSchemePlan_ProductAssetInstrumentSubSubCategory]
GO
/****** Object:  ForeignKey [FK_ProductAMCSchemePlanCorpAction_ProductAMCSchemePlan]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductAMCSchemePlanCorpAction]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemePlanCorpAction_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO
ALTER TABLE [dbo].[ProductAMCSchemePlanCorpAction] CHECK CONSTRAINT [FK_ProductAMCSchemePlanCorpAction_ProductAMCSchemePlan]
GO
/****** Object:  ForeignKey [FK_ProductAMCSchemePlanPrice_ProductAMCSchemePlan]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductAMCSchemePlanPrice]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemePlanPrice_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO
ALTER TABLE [dbo].[ProductAMCSchemePlanPrice] CHECK CONSTRAINT [FK_ProductAMCSchemePlanPrice_ProductAMCSchemePlan]
GO
/****** Object:  ForeignKey [FK_ProductAssetInstrumentCategory_ProductAssetGroup]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductAssetInstrumentCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductAssetInstrumentCategory_ProductAssetGroup] FOREIGN KEY([PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetGroup] ([PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[ProductAssetInstrumentCategory] CHECK CONSTRAINT [FK_ProductAssetInstrumentCategory_ProductAssetGroup]
GO
/****** Object:  ForeignKey [FK_ProductAssetInstrumentSubCategory_ProductAssetInstrumentCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductAssetInstrumentSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductAssetInstrumentSubCategory_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[ProductAssetInstrumentSubCategory] CHECK CONSTRAINT [FK_ProductAssetInstrumentSubCategory_ProductAssetInstrumentCategory]
GO
/****** Object:  ForeignKey [FK_ProductAssetInstrumentSubSubCategory_ProductAssetInstrumentSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductAssetInstrumentSubSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductAssetInstrumentSubSubCategory_ProductAssetInstrumentSubCategory] FOREIGN KEY([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubCategory] ([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[ProductAssetInstrumentSubSubCategory] CHECK CONSTRAINT [FK_ProductAssetInstrumentSubSubCategory_ProductAssetInstrumentSubCategory]
GO
/****** Object:  ForeignKey [FK_ProductEquityMaster_ProductAssetInstrumentSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductEquityMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductEquityMaster_ProductAssetInstrumentSubCategory] FOREIGN KEY([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubCategory] ([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[ProductEquityMaster] CHECK CONSTRAINT [FK_ProductEquityMaster_ProductAssetInstrumentSubCategory]
GO
/****** Object:  ForeignKey [FK_ProductEquityMaster_ProductCAPClassification]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductEquityMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductEquityMaster_ProductCAPClassification] FOREIGN KEY([PMCC_MarketCapClassificationCode])
REFERENCES [dbo].[ProductMarketCapClassification] ([PMCC_MarketCapClassificationCode])
GO
ALTER TABLE [dbo].[ProductEquityMaster] CHECK CONSTRAINT [FK_ProductEquityMaster_ProductCAPClassification]
GO
/****** Object:  ForeignKey [FK_ProductEquityMaster_ProductEquityMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductEquityMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductEquityMaster_ProductEquityMaster] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO
ALTER TABLE [dbo].[ProductEquityMaster] CHECK CONSTRAINT [FK_ProductEquityMaster_ProductEquityMaster]
GO
/****** Object:  ForeignKey [FK_ProductEquityPrice_ProductEquityMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductEquityPrice]  WITH CHECK ADD  CONSTRAINT [FK_ProductEquityPrice_ProductEquityMaster] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO
ALTER TABLE [dbo].[ProductEquityPrice] CHECK CONSTRAINT [FK_ProductEquityPrice_ProductEquityMaster]
GO
/****** Object:  ForeignKey [FK_ProductEquityScripMapping_ProductEquityMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductEquityScripMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProductEquityScripMapping_ProductEquityMaster] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO
ALTER TABLE [dbo].[ProductEquityScripMapping] CHECK CONSTRAINT [FK_ProductEquityScripMapping_ProductEquityMaster]
GO
/****** Object:  ForeignKey [FK_ProductGlobalSectorSubCategory_ProductGlobalSectorCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductGlobalSectorSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductGlobalSectorSubCategory_ProductGlobalSectorCategory] FOREIGN KEY([PGSC_SectorCategoryCode])
REFERENCES [dbo].[ProductGlobalSectorCategory] ([PGSC_SectorCategoryCode])
GO
ALTER TABLE [dbo].[ProductGlobalSectorSubCategory] CHECK CONSTRAINT [FK_ProductGlobalSectorSubCategory_ProductGlobalSectorCategory]
GO
/****** Object:  ForeignKey [FK_ProductGlobalSectorSubSubCategory_ProductGlobalSectorSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[ProductGlobalSectorSubSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductGlobalSectorSubSubCategory_ProductGlobalSectorSubCategory] FOREIGN KEY([PGSSC_SectorSubCategoryCode], [PGSC_SectorCategoryCode])
REFERENCES [dbo].[ProductGlobalSectorSubCategory] ([PGSSC_SectorSubCategoryCode], [PGSC_SectorCategoryCode])
GO
ALTER TABLE [dbo].[ProductGlobalSectorSubSubCategory] CHECK CONSTRAINT [FK_ProductGlobalSectorSubSubCategory_ProductGlobalSectorSubCategory]
GO
/****** Object:  ForeignKey [FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerSubType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerSubType] FOREIGN KEY([XCST_CustomerSubTypeCode])
REFERENCES [dbo].[XMLCustomerSubType] ([XCST_CustomerSubTypeCode])
GO
ALTER TABLE [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping] CHECK CONSTRAINT [FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerSubType]
GO
/****** Object:  ForeignKey [FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerType] FOREIGN KEY([XCT_CustomerTypeCode])
REFERENCES [dbo].[XMLCustomerType] ([XCT_CustomerTypeCode])
GO
ALTER TABLE [dbo].[WerpCAMSCustomerTypeDataTranslatorMapping] CHECK CONSTRAINT [FK_WerpCAMSCustomerTypeDataTranslatorMapping_XMLCustomerType]
GO
/****** Object:  ForeignKey [FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerSubType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpCustomerTypeDataTranslatorMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerSubType] FOREIGN KEY([XCST_CustomerSubTypeCode])
REFERENCES [dbo].[XMLCustomerSubType] ([XCST_CustomerSubTypeCode])
GO
ALTER TABLE [dbo].[WerpCustomerTypeDataTranslatorMapping] CHECK CONSTRAINT [FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerSubType]
GO
/****** Object:  ForeignKey [FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpCustomerTypeDataTranslatorMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerType] FOREIGN KEY([XCT_CustomerTypeCode])
REFERENCES [dbo].[XMLCustomerType] ([XCT_CustomerTypeCode])
GO
ALTER TABLE [dbo].[WerpCustomerTypeDataTranslatorMapping] CHECK CONSTRAINT [FK_WerpCustomerTypeDataTranslatorMapping_XMLCustomerType]
GO
/****** Object:  ForeignKey [FK_WerpExternalSourceHeaderMaster_XMLExternalFileType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpExternalSourceHeaderMaster]  WITH CHECK ADD  CONSTRAINT [FK_WerpExternalSourceHeaderMaster_XMLExternalFileType] FOREIGN KEY([XESFT_FileTypeId])
REFERENCES [dbo].[XMLExternalSourceFileType] ([XESFT_FileTypeId])
GO
ALTER TABLE [dbo].[WerpExternalSourceHeaderMaster] CHECK CONSTRAINT [FK_WerpExternalSourceHeaderMaster_XMLExternalFileType]
GO
/****** Object:  ForeignKey [FK_WerpKarvyBankModeOfHoldingDataTranslatorMapping_XMLModeOfHolding]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpKarvyBankModeOfHoldingDataTranslatorMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpKarvyBankModeOfHoldingDataTranslatorMapping_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO
ALTER TABLE [dbo].[WerpKarvyBankModeOfHoldingDataTranslatorMapping] CHECK CONSTRAINT [FK_WerpKarvyBankModeOfHoldingDataTranslatorMapping_XMLModeOfHolding]
GO
/****** Object:  ForeignKey [FK_WerpProfileFilterCategory_XMLCustomerType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpProfileFilterCategory]  WITH CHECK ADD  CONSTRAINT [FK_WerpProfileFilterCategory_XMLCustomerType] FOREIGN KEY([XCT_CustomerTypeCode])
REFERENCES [dbo].[XMLCustomerType] ([XCT_CustomerTypeCode])
GO
ALTER TABLE [dbo].[WerpProfileFilterCategory] CHECK CONSTRAINT [FK_WerpProfileFilterCategory_XMLCustomerType]
GO
/****** Object:  ForeignKey [FK_WerpProofMandatoryLookup_WerpProfileFilterCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpProofMandatoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_WerpProofMandatoryLookup_WerpProfileFilterCategory] FOREIGN KEY([WPFC_FilterCategoryCode])
REFERENCES [dbo].[WerpProfileFilterCategory] ([WPFC_FilterCategoryCode])
GO
ALTER TABLE [dbo].[WerpProofMandatoryLookup] CHECK CONSTRAINT [FK_WerpProofMandatoryLookup_WerpProfileFilterCategory]
GO
/****** Object:  ForeignKey [FK_WerpProofMandatoryLookup_XMLProof]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpProofMandatoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_WerpProofMandatoryLookup_XMLProof] FOREIGN KEY([XP_ProofCode])
REFERENCES [dbo].[XMLProof] ([XP_ProofCode])
GO
ALTER TABLE [dbo].[WerpProofMandatoryLookup] CHECK CONSTRAINT [FK_WerpProofMandatoryLookup_XMLProof]
GO
/****** Object:  ForeignKey [FK_QuestionChoice_QuestionMaster]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpQuestionChoice]  WITH CHECK ADD  CONSTRAINT [FK_QuestionChoice_QuestionMaster] FOREIGN KEY([WQM_QuestionId])
REFERENCES [dbo].[WerpQuestionMaster] ([WQM_QuestionId])
GO
ALTER TABLE [dbo].[WerpQuestionChoice] CHECK CONSTRAINT [FK_QuestionChoice_QuestionMaster]
GO
/****** Object:  ForeignKey [FK_WerpULIPSubPlan_WerpULIPPlan]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpULIPSubPlan]  WITH CHECK ADD  CONSTRAINT [FK_WerpULIPSubPlan_WerpULIPPlan] FOREIGN KEY([WUP_ULIPPlanCode])
REFERENCES [dbo].[WerpULIPPlan] ([WUP_ULIPPlanCode])
GO
ALTER TABLE [dbo].[WerpULIPSubPlan] CHECK CONSTRAINT [FK_WerpULIPSubPlan_WerpULIPPlan]
GO
/****** Object:  ForeignKey [FK_WerpUploadFieldMapping_WerpUploadXMLFileType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpUploadFieldMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpUploadFieldMapping_WerpUploadXMLFileType] FOREIGN KEY([WUXFT_XMLFileTypeId])
REFERENCES [dbo].[WerpUploadXMLFileType] ([WUXFT_XMLFileTypeId])
GO
ALTER TABLE [dbo].[WerpUploadFieldMapping] CHECK CONSTRAINT [FK_WerpUploadFieldMapping_WerpUploadXMLFileType]
GO
/****** Object:  ForeignKey [FK_WerpValueResearchAssetClassificationMapping_ProductAssetInstrumentSubSubCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[WerpValueResearchAssetClassificationMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpValueResearchAssetClassificationMapping_ProductAssetInstrumentSubSubCategory] FOREIGN KEY([PAISSC_AssetInstrumentSubSubCategoryCode], [PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubSubCategory] ([PAISSC_AssetInstrumentSubSubCategoryCode], [PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO
ALTER TABLE [dbo].[WerpValueResearchAssetClassificationMapping] CHECK CONSTRAINT [FK_WerpValueResearchAssetClassificationMapping_ProductAssetInstrumentSubSubCategory]
GO
/****** Object:  ForeignKey [FK_XMLAdviserLOBClassification_XMLAdviserLOBAssetGroup]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[XMLAdviserLOBClassification]  WITH CHECK ADD  CONSTRAINT [FK_XMLAdviserLOBClassification_XMLAdviserLOBAssetGroup] FOREIGN KEY([XALAG_LOBAssetGroupsCode])
REFERENCES [dbo].[XMLAdviserLOBAssetGroup] ([XALAG_LOBAssetGroupsCode])
GO
ALTER TABLE [dbo].[XMLAdviserLOBClassification] CHECK CONSTRAINT [FK_XMLAdviserLOBClassification_XMLAdviserLOBAssetGroup]
GO
/****** Object:  ForeignKey [FK_XMLAdviserLOBClassification_XMLAdviserLOBCategory]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[XMLAdviserLOBClassification]  WITH CHECK ADD  CONSTRAINT [FK_XMLAdviserLOBClassification_XMLAdviserLOBCategory] FOREIGN KEY([XALC_LOBCategoryCode])
REFERENCES [dbo].[XMLAdviserLOBCategory] ([XALC_LOBCategoryCode])
GO
ALTER TABLE [dbo].[XMLAdviserLOBClassification] CHECK CONSTRAINT [FK_XMLAdviserLOBClassification_XMLAdviserLOBCategory]
GO
/****** Object:  ForeignKey [FK_XMLAdviserLOBClassification_XMLAdviserLOBEquitySegment]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[XMLAdviserLOBClassification]  WITH CHECK ADD  CONSTRAINT [FK_XMLAdviserLOBClassification_XMLAdviserLOBEquitySegment] FOREIGN KEY([XALES_SegmentCode])
REFERENCES [dbo].[XMLAdviserLOBEquitySegment] ([XALES_SegmentCode])
GO
ALTER TABLE [dbo].[XMLAdviserLOBClassification] CHECK CONSTRAINT [FK_XMLAdviserLOBClassification_XMLAdviserLOBEquitySegment]
GO
/****** Object:  ForeignKey [FK_XMLCustomerSubType_XMLCustomerType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[XMLCustomerSubType]  WITH CHECK ADD  CONSTRAINT [FK_XMLCustomerSubType_XMLCustomerType] FOREIGN KEY([XCT_CustomerTypeCode])
REFERENCES [dbo].[XMLCustomerType] ([XCT_CustomerTypeCode])
GO
ALTER TABLE [dbo].[XMLCustomerSubType] CHECK CONSTRAINT [FK_XMLCustomerSubType_XMLCustomerType]
GO
/****** Object:  ForeignKey [FK_XMLExternalSource_XMLExternalSource]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[XMLExternalSource]  WITH CHECK ADD  CONSTRAINT [FK_XMLExternalSource_XMLExternalSource] FOREIGN KEY([XES_SourceCode])
REFERENCES [dbo].[XMLExternalSource] ([XES_SourceCode])
GO
ALTER TABLE [dbo].[XMLExternalSource] CHECK CONSTRAINT [FK_XMLExternalSource_XMLExternalSource]
GO
/****** Object:  ForeignKey [FK_XMLExternalSourceFileType_WerpUploadXMLFileType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[XMLExternalSourceFileType]  WITH CHECK ADD  CONSTRAINT [FK_XMLExternalSourceFileType_WerpUploadXMLFileType] FOREIGN KEY([WUXFT_XMLFileTypeId])
REFERENCES [dbo].[WerpUploadXMLFileType] ([WUXFT_XMLFileTypeId])
GO
ALTER TABLE [dbo].[XMLExternalSourceFileType] CHECK CONSTRAINT [FK_XMLExternalSourceFileType_WerpUploadXMLFileType]
GO
/****** Object:  ForeignKey [FK_XMLExternalSourceFileType_XMLExternalSource]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[XMLExternalSourceFileType]  WITH CHECK ADD  CONSTRAINT [FK_XMLExternalSourceFileType_XMLExternalSource] FOREIGN KEY([XES_SourceCode])
REFERENCES [dbo].[XMLExternalSource] ([XES_SourceCode])
GO
ALTER TABLE [dbo].[XMLExternalSourceFileType] CHECK CONSTRAINT [FK_XMLExternalSourceFileType_XMLExternalSource]
GO
/****** Object:  ForeignKey [FK_XMLExternalSourceFileType_XMLExternalSourceFileType]    Script Date: 06/12/2009 18:44:52 ******/
ALTER TABLE [dbo].[XMLExternalSourceFileType]  WITH CHECK ADD  CONSTRAINT [FK_XMLExternalSourceFileType_XMLExternalSourceFileType] FOREIGN KEY([XESFT_FileTypeId])
REFERENCES [dbo].[XMLExternalSourceFileType] ([XESFT_FileTypeId])
GO
ALTER TABLE [dbo].[XMLExternalSourceFileType] CHECK CONSTRAINT [FK_XMLExternalSourceFileType_XMLExternalSourceFileType]
GO
