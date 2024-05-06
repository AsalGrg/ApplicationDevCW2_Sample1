import "./App.css";
import { Route, RouterProvider, createRoutesFromElements } from "react-router";
import { MantineProvider, createTheme } from "@mantine/core";
import { createBrowserRouter } from "react-router-dom";
import UserLayout from "./components/layout/UserLayout";
import Home from "./pages/home/Home";
import "@mantine/core/styles.css";
import BlogDescription from "./pages/Blog description/BlogDescription";
import Login from "./pages/login/Login";
import Register from "./pages/login/Register";
import VerifyEmail from "./pages/verify email/VerifyEmail";
import ForgotPassword from "./pages/forgot password/ForgotPassword";
import VerfiyEmailForgotPassword from "./pages/forgot password/VerfiyEmailForgotPassword";

function App() {
  const theme = createTheme({
    /** Put your mantine theme override here */
  });

  const router = createBrowserRouter(
    createRoutesFromElements(
      <Route path="/">
        <Route path="/login" element={<Login />} />
        <Route path="/Register" element={<Register />} />
        <Route path="/verifyEmail/:token/:email" element={<VerifyEmail />} />
        <Route path="/forgotPassword" element={<ForgotPassword />} />
        <Route path="/forgotPassword/verify-email/:token/:email" element={<VerfiyEmailForgotPassword/>} />
        <Route element={<UserLayout />}>
          <Route path="/register" element={<BlogDescription />} />
          <Route index element={<Home />} />
          <Route path="/blog/:id" element={<BlogDescription />} />
        </Route>
        {/* <Route element={<GlobalLayout />}>
          <Route path="/blog/:id" element={<BlogDescription />} />
          <Route path="/create/blog" element={<CreateBlog />} />
        </Route> */}
      </Route>
    )
  );

  return (
    <MantineProvider theme={theme}>
      <RouterProvider router={router} />
    </MantineProvider>
  );
}

export default App;
