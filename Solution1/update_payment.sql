UPDATE `Order` SET paymentMethod='VisaOnDelivery' WHERE paymentMethod='Card';
SELECT COUNT(*) AS CardRowsRemaining FROM `Order` WHERE paymentMethod='Card';
SELECT COUNT(*) AS VisaRows FROM `Order` WHERE paymentMethod='VisaOnDelivery';
