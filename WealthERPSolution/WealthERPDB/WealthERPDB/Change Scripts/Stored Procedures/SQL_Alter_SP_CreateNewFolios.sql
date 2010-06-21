

ALTER PROCEDURE [dbo].[SP_CreateNewFolios]
@portfolioId int,
@folioNum VARCHAR(50),
@userId int
AS
BEGIN
insert into CustomerMutualFundAccount(PAG_AssetGroupCode,CP_PortfolioId,CMFA_FolioNum,CMFA_CreatedOn,CMFA_CreatedBy,CMFA_ModifiedBy,CMFA_ModifiedOn) values('MF',@portfolioId,@folioNum,current_timestamp,@userId,@userId,current_timestamp)
	
END

 