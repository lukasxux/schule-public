# Funktionen: Ableitung, Integral und ihre Bedeutung

## Inhaltsverzeichnis
- [Funktionen: Ableitung, Integral und ihre Bedeutung](#funktionen-ableitung-integral-und-ihre-bedeutung)
  - [Inhaltsverzeichnis](#inhaltsverzeichnis)
  - [Einleitung](#einleitung)
  - [Funktionen und ihre Grundlagen](#funktionen-und-ihre-grundlagen)
  - [Ableitung: Bedeutung und Anwendung](#ableitung-bedeutung-und-anwendung)
    - [Physikalische Bedeutung:](#physikalische-bedeutung)
  - [Integral: Bedeutung und Anwendung](#integral-bedeutung-und-anwendung)
    - [Physikalische Bedeutung:](#physikalische-bedeutung-1)
  - [Beispiel: Bewegung mit (s(t)), (v(t)), (a(t))](#beispiel-bewegung-mit-st-vt-at)
  - [Zusammenfassung](#zusammenfassung)

---

## Einleitung

Dieses Dokument beschreibt die Grundlagen von Funktionen, Ableitung und Integral. Es wird auf deren Bedeutung, die Berechnung und konkrete Anwendungen in der Physik eingegangen. Insbesondere betrachten wir Bewegungsfunktionen wie:
- \( s(t) \): Ort (Position) in Abhängigkeit von der Zeit  
- \( v(t) \): Geschwindigkeit in Abhängigkeit von der Zeit  
- \( a(t) \): Beschleunigung in Abhängigkeit von der Zeit  

---

## Funktionen und ihre Grundlagen

Eine **Funktion** ordnet jedem Wert \(x\) aus der Definitionsmenge einen Wert \(y\) aus der Zielmenge zu:

\[
f: x \mapsto y = f(x)
\]

**Beispiel:**  
\[
f(x) = x^2 \quad \text{ist eine quadratische Funktion.}
\]

---

## Ableitung: Bedeutung und Anwendung

Die **Ableitung** einer Funktion \(f(x)\) beschreibt die Steigung der Funktion an einer bestimmten Stelle. Sie zeigt, wie schnell sich die Funktion ändert.

**Definition der Ableitung:**
\[
f'(x) = \lim_{h \to 0} \frac{f(x+h) - f(x)}{h}
\]

### Physikalische Bedeutung:
- \( v(t) = s'(t) \): Die Geschwindigkeit ist die Ableitung des Ortes nach der Zeit.  
- \( a(t) = v'(t) = s''(t) \): Die Beschleunigung ist die Ableitung der Geschwindigkeit nach der Zeit.

---

## Integral: Bedeutung und Anwendung

Das **Integral** beschreibt die Fläche unter einer Kurve. Es wird verwendet, um Größen wie die zurückgelegte Strecke oder Gesamtveränderungen zu berechnen.

**Bestimmtes Integral:**
\[
\int_a^b f(x) \, dx = F(b) - F(a)
\]

**Unbestimmtes Integral:**
\[
\int f(x) \, dx = F(x) + C \quad \text{(Stammfunktion mit Konstante \(C\))}.
\]

### Physikalische Bedeutung:
- \( s(t) = \int v(t) \, dt \): Die Position ist das Integral der Geschwindigkeit.  
- \( v(t) = \int a(t) \, dt \): Die Geschwindigkeit ist das Integral der Beschleunigung.

---

## Beispiel: Bewegung mit \(s(t)\), \(v(t)\), \(a(t)\)

Angenommen, die Beschleunigung \(a(t)\) ist konstant:
\[
a(t) = 2 \, \text{m/s}^2
\]

1. **Berechnung der Geschwindigkeit \(v(t)\):**
   \[
   v(t) = \int a(t) \, dt = 2t + C_1
   \]
   \(C_1\) ist die Anfangsgeschwindigkeit \(v_0\).

2. **Berechnung der Position \(s(t)\):**
   \[
   s(t) = \int v(t) \, dt = \int (2t + C_1) \, dt = t^2 + C_1 t + C_2
   \]
   \(C_2\) ist der Anfangsort \(s_0\).

---

## Zusammenfassung

- Die **Ableitung** zeigt die momentane Änderungsrate (z.B. Steigung der Tangente).  
- Das **Integral** berechnet die Fläche unter der Kurve und summiert Änderungen.  
- In der Physik werden \(s(t)\), \(v(t)\) und \(a(t)\) genutzt, um Bewegungsabläufe zu beschreiben.

**Wichtige Formeln:**
1. \( v(t) = s'(t) \)
2. \( a(t) = v'(t) \)
3. \( s(t) = \int v(t) \, dt \)
