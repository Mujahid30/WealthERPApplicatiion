/****** Object:  StoredProcedure [dbo].[SP_GetProductAmc]    Script Date: 06/24/2009 12:03:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductAmc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetProductAmc]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductAmc]    Script Date: 06/24/2009 12:03:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductAmc]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetProductAmc]
AS
SELECT * FROM dbo.ProductAMC' 
END
GO
