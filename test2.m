%%laba 1
%1 Исходные данные
clear all;
close all;
K = 2000;
n = 1;
b = 10;
M = b;
D = b^2;
%2 Генерация реализаций СВ
alf = rand(n,K);
X = -b*log(alf);
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


