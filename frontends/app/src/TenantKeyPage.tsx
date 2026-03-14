import React, { useState } from "react";
import styles from "./TenantKeyPage.module.css";

const API_URL = import.meta.env.VITE_CORE_API_BASE || "http://localhost:5000";

export default function TenantKeyPage() {
  const [tenantKey, setTenantKey] = useState("");
  const [jsonResponse, setJsonResponse] = useState<object | null>(null);
  const [textResponse, setTextResponse] = useState<string | null>(null);
  const [statusCode, setStatusCode] = useState<number | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setJsonResponse(null);
    setTextResponse(null);
    setStatusCode(null);
    try {
      const url = new URL(API_URL);
      url.hostname = `${tenantKey}.${url.hostname}`;
      const res = await fetch(
        `${url.toString().replace(/\/$/, "")}/api/resource`,
        {
          method: "GET",
          headers: {
            "CoreAPI-Tenant-Key": tenantKey,
          },
        },
      );
      setStatusCode(res.status);
      const text = await res.text();
      if (res.ok) {
        const jsonData = JSON.parse(text);
        setJsonResponse(jsonData);
      } else {
        setTextResponse(text);
      }
    } catch (err) {
      setError(
        (err instanceof Error ? err.message : String(err)) || "Request failed",
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className={styles.container}>
      <h2 className={styles.title}>Send Tenant Key Header</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          value={tenantKey}
          onChange={(e) => setTenantKey(e.target.value)}
          placeholder="Enter tenant key"
          className={styles.input}
        />
        <button
          type="submit"
          disabled={loading || !tenantKey}
          className={styles.button}
        >
          {loading ? "Sending..." : "Send"}
        </button>
      </form>
      {statusCode === 200 &&
        jsonResponse &&
        Array.isArray(jsonResponse) &&
        jsonResponse.length > 0 && (
          <table className={styles.table}>
            <thead>
              <tr>
                {Object.keys(jsonResponse[0] as Record<string, unknown>).map(
                  (key) => (
                    <th key={key}>{key}</th>
                  ),
                )}
              </tr>
            </thead>
            <tbody>
              {jsonResponse.map((row: unknown, idx: number) => (
                <tr key={idx}>
                  {Object.values(row as Record<string, unknown>).map(
                    (value, cidx) => (
                      <td key={cidx}>{JSON.stringify(value)}</td>
                    ),
                  )}
                </tr>
              ))}
            </tbody>
          </table>
        )}
      {statusCode && statusCode !== 200 && textResponse && (
        <div className={styles.response}>Response: {textResponse}</div>
      )}
      {error && <div className={styles.error}>Error: {error}</div>}
    </div>
  );
}
