/****** Object:  StoredProcedure [dbo].[SP_GetCustomerGovtSavingsAccount]    Script Date: 06/24/2009 12:00:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerGovtSavingsAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerGovtSavingsAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerGovtSavingsAccount]    Script Date: 06/24/2009 12:00:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerGovtSavingsAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerGovtSavingsAccount]

@CGSA_AccountId INT

AS

SELECT CGSA.*,PAIC.PAIC_AssetInstrumentCategoryName FROM dbo.CustomerGovtSavingAccount AS CGSA 
INNER JOIN dbo.ProductAssetInstrumentCategory AS PAIC 
ON CGSA.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
WHERE CGSA_AccountId=@CGSA_AccountId

 ' 
END
GO
