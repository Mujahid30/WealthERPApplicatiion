/****** Object:  StoredProcedure [dbo].[SP_GetCustomerEQAccounts]    Script Date: 06/24/2009 11:59:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerEQAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerEQAccounts]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerEQAccounts]    Script Date: 06/24/2009 11:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerEQAccounts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerEQAccounts]
@CP_PortfolioId INT,
@PAG_AssetGroupCode varchar(5)


AS

SELECT * FROM CustomerEquityTradeAccount WHERE CP_PortfolioId=@CP_PortfolioId AND PAG_AssetGroupCode=@PAG_AssetGroupCode' 
END
GO
