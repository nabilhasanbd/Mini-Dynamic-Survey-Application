import type { Metadata } from "next";
import "./globals.css";

export const metadata: Metadata = {
  title: "SurveyMaster - Dynamic Surveys",
  description: "Create, share, and analyze dynamic surveys with ease.",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <main className="container">
          {children}
        </main>
      </body>
    </html>
  );
}
