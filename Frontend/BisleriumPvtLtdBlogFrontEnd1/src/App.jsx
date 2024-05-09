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
import AddBlog from "./pages/add blog/AddBlog";
import UserProfile from "./pages/user profile/UserProfile";
import AdminLayout from "./components/layout/AdminLayout";
import AdminHome from "./pages/admin/AdminHome";
import '@mantine/dates/styles.css';
import RegisterAdmin from "./pages/admin/RegisterAdmin";
import Profile from "./pages/admin/Profile";

function App() {
  const theme = createTheme({
    /** Put your mantine theme override here */
  });

  const router = createBrowserRouter(
    createRoutesFromElements(
      <Route path="/">
        <Route path="/login" element={<Login />} />
        <Route path="/Register" element={<Register />} />
        <Route path="/verifyEmail" element={<VerifyEmail />} />
        <Route path="/forgotPassword" element={<ForgotPassword />} />
        <Route
          path="/forgotPassword/verify-email"
          element={<VerfiyEmailForgotPassword />}
        />
        <Route element={<UserLayout />}>
          <Route index element={<Home />} />
          <Route path="/blog/:id" element={<BlogDescription />} />
          <Route path="/profile" element={<UserProfile />} />
          <Route path="/blog/:type/:id" element={<AddBlog />} />
        </Route>

        <Route element={<AdminLayout />}>
          <Route path="/admin/home" element={<AdminHome />} />
          <Route path="/admin/add-admin" element={<RegisterAdmin />} />
          <Route path="/admin/profile" element={<Profile />} />
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
