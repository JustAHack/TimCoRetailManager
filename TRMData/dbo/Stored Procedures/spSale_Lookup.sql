﻿CREATE PROCEDURE [dbo].[spSale_Lookup]
	@CashierId nvarchar(128),
	@SaleDate DateTime2
AS

BEGIN

	SET NOCOUNT ON;

	SELECT Id
	FROM [dbo].[Sale]
	WHERE CashierId = @CashierId AND SaleDate = @SaleDate;

END