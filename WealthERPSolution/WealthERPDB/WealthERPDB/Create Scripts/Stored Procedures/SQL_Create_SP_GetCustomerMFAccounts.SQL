/****** Object:  StoredProcedure [dbo].[SP_GetCustomerMFAccounts]    Script Date: 06/24/2009 12:00:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerMFAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerMFAccounts]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerMFAccounts]    Script Date: 06/24/2009 12:00:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerMFAccounts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerMFAccounts]
@CP_PortfolioId INT,
@PAG_AssetGroupCode varchar(5) 

AS

SELECT * FROM CustomerMutualFundAccount WHERE CP_PortfolioId=@CP_PortfolioId AND PAG_AssetGroupCode=@PAG_AssetGroupCode' 
END
GO
