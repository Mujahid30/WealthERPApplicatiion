/****** Object:  StoredProcedure [dbo].[SP_GetCustomerEQDPAccounts]    Script Date: 06/24/2009 11:59:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerEQDPAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerEQDPAccounts]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerEQDPAccounts]    Script Date: 06/24/2009 11:59:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerEQDPAccounts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerEQDPAccounts]
@CP_PortfolioId INT


AS

SELECT * FROM CustomerEquityDematAccount WHERE CP_PortfolioId=@CP_PortfolioId' 
END
GO
