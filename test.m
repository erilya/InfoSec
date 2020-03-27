%%laba 1
%1 Исходные данные
clear all
close all
K = 2000;
n = 1;
a = 0;
b = 10;
M = a + b/2;
D = b^2/12;
%2 Генерация реализаций СВ
alf = rand(n,K);
X = a + b*alf;
%3 выборочная характеристика
ms = zeros(1,K);
ds = zeros(1,K);
for k = 1:K
    ms(k) = mean(X(1:k));
    ds(k) = var(X(1:k));
end
%4 Визуализация
figure; hold on;
plot(1:K, ms);
plot(1:K,M*ones(1,K));
title('Выборочное среднее от числа реализаций СВ');

figure; hold on;
plot(1:K, ds);
plot(1:K,D*ones(1,K));
title('Дисперсия от числа реализаций СВ');


